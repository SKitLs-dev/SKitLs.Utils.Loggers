using SKitLs.Data.IO.Shortcuts;
using SKitLs.Utils.Localizations.Languages;
using SKitLs.Utils.Localizations.Localizators;
using SKitLs.Utils.Localizations.Model;
using SKitLs.Utils.Loggers.Formatting;
using SKitLs.Utils.Loggers.Prototype;
using System.Reflection.Metadata.Ecma335;

namespace SKitLs.Utils.Loggers.Logging
{
    public abstract class LoggerBase : ILogger
    {
        public const string PrintAllModeName = "AllMode";
        public static LoggerMode PrintAllMode => (LoggerMode)PrintAllModeName;

        protected IFormatter Formatter { get; set; }

        public ILocalizator? Localizator { get; set; }

        public LoggerBase(ILocalizator? localizator, IFormatter? formatter)
        {
            Localizator = localizator;
            Formatter = formatter ?? new Formatter();

            Configure();
        }

        protected LoggerMode? Resolve(string? name) => ModesEnabling.Where(x => x.Key.Name.Equals(name)).FirstOrDefault().Key;

        #region Configuration
        private int DeepToLevel { get; set; } = -1;

        public void UseLeveling(int maxDeepness) => DeepToLevel = maxDeepness;

        private Dictionary<LoggerMode, bool> ModesEnabling { get; set; } = [];

        public void Enable(LoggerMode loggerMode)
        {
            if (!ModesEnabling.TryAdd(loggerMode, true))
                ModesEnabling[loggerMode] = true;
        }

        public void Disable(LoggerMode loggerMode)
        {
            if (!ModesEnabling.TryAdd(loggerMode, false))
                ModesEnabling[loggerMode] = false;
        }

        public async Task FromFileAsync(string configPath = "Resources/Logging/levels.txt")
        {
            var enables = await HotIO.LoadPairsAsync(configPath, "$");
            enables.Select(Parse).ToList().ForEach(Enable);

            static LoggerMode Parse(KeyValuePair<string, string> config)
            {
                if (!int.TryParse(config.Value, out var level))
                    throw new Exception("Unable to parse logger config.");
                return new LoggerMode(config.Key, level);
            }
        }

        protected virtual async void Configure()
        {
            Enable(PrintAllMode);
            await FromFileAsync();
        }
        #endregion

        public virtual bool ShouldPrint(LoggerMode loggerMode)
        {
            if (DeepToLevel > -1)
                return loggerMode.Level <= DeepToLevel;
            if (ModesEnabling.TryGetValue(loggerMode, out var @switch))
                return @switch;
            return false;
        }

        public virtual bool Log(LogEvent log, LanguageCode language = LanguageCode.EN)
        {
            if (!ShouldPrint(log.LoggerMode))
                return false;

            var localizedMessage = Localizator?.ResolveString(language, log.Message, false);
            if (localizedMessage is not null)
                log.Message = localizedMessage;
            log.Message = $"[{log.Time:HH:mm:ss}] {log.Message.LocalizationKey}";
            Formatter.Format(log);
            return true;
        }

        public virtual void Log(LocalSet message, string logType, LoggerMode? loggerMode = null)
        {
            if (DeepToLevel < 0 && loggerMode is not null)
                loggerMode = Resolve(loggerMode.Name);

            var log = new LogEvent(message, logType, loggerMode ?? PrintAllMode);
            Log(log);
        }

        public virtual void Log(string message, string logType = nameof(LogType.Message), LoggerMode? loggerMode = null) => Log((LocalSet)message, logType, loggerMode);

        public abstract void DropEmpty();

        public virtual void Error(LocalSet message, LoggerMode? loggerMode = null) => Log(message, LogType.Error.ToString(), loggerMode);

        public virtual void Warn(LocalSet message, LoggerMode? loggerMode = null) => Log(message, LogType.Warning.ToString(), loggerMode);

        public virtual void Success(LocalSet message, LoggerMode? loggerMode = null) => Log(message, LogType.Successful.ToString(), loggerMode);

        public virtual void System(LocalSet message, LoggerMode? loggerMode = null) => Log(message, LogType.System.ToString(), loggerMode);

        public virtual void Inform(LocalSet message, LoggerMode? loggerMode = null) => Log(message, LogType.Information.ToString(), loggerMode);
    }
}