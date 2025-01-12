using SKitLs.Data.IO;
using SKitLs.Utils.Localizations.Languages;
using SKitLs.Utils.Localizations.Localizators;
using SKitLs.Utils.Localizations.Model;
using SKitLs.Utils.Loggers.Formatting;
using SKitLs.Utils.Loggers.Prototype;

namespace SKitLs.Utils.Loggers.Logging
{
    // LogEvent.UseUtc

    /// <summary>
    /// Abstract base class for logging functionality, providing methods for configuring and logging various messages.
    /// </summary>
    public abstract class LoggerBase : ILogger
    {
        /// <summary>
        /// The name used to represent the mode that prints all log levels.
        /// </summary>
        public const string PrintAllModeName = "AllMode";

        /// <summary>
        /// Gets the logger mode that enables printing of all log messages.
        /// </summary>
        public static LoggerMode PrintAllMode => (LoggerMode)PrintAllModeName;

        /// <summary>
        /// Get or sets the path to the leveling definition.
        /// </summary>
        public string? LevelingFilePath { get; set; }

        /// <summary>
        /// Gets or sets the formatter responsible for formatting log events.
        /// </summary>
        protected IFormatter Formatter { get; set; }

        /// <summary>
        /// Gets or sets the localizator used to localize log messages.
        /// </summary>
        public ILocalizator? Localizator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerBase"/> class with the specified localizator and formatter.
        /// </summary>
        /// <param name="levelingFilePath">The path to the leveling definition</param>
        /// <param name="localizator">An optional localizator for message localization. Defaults to a new <see cref="GateLocalizator"/> if <see langword="null"/>.</param>
        /// <param name="formatter">An optional formatter for formatting logs. Defaults to a new <see cref="Formatting.Formatter"/> if <see langword="null"/>.</param>
        public LoggerBase(string? levelingFilePath, ILocalizator? localizator, IFormatter? formatter)
        {
            LevelingFilePath = levelingFilePath;
            Localizator = localizator ?? new StoredLocalizator();
            Formatter = formatter ?? new Formatter(Localizator);

            Configure();
        }

        /// <summary>
        /// Resolves a <see cref="LoggerMode"/> by its name from the enabled modes.
        /// </summary>
        /// <param name="name">The name of the logger mode to resolve.</param>
        /// <returns>The resolved logger mode, or null if not found.</returns>
        protected LoggerMode? Resolve(string? name) => ModesEnabling.Where(x => x.Key.Name.Equals(name)).FirstOrDefault().Key;

        #region Configuration
        /// <summary>
        /// Gets or sets the default logger mode used when no specific mode is provided.
        /// </summary>
        public LoggerMode? DefaultLoggerMode { get; set; } = LoggerMode.Essential();

        /// <summary>
        /// Gets or sets the maximum logging depth, which controls the maximum log level to print.
        /// </summary>
        private int DeepToLevel { get; set; } = -1;

        /// <summary>
        /// Sets the maximum level of deepness for logging, controlling which logs will be printed based on their level.
        /// </summary>
        /// <param name="maxDeepness">The maximum log level allowed to print.</param>
        public void UseLeveling(int maxDeepness) => DeepToLevel = maxDeepness;

        /// <summary>
        /// Gets or sets the dictionary that tracks enabled or disabled logger modes.
        /// </summary>
        private Dictionary<LoggerMode, bool> ModesEnabling { get; set; } = [];

        /// <summary>
        /// Enables logging for a specific logger mode.
        /// </summary>
        /// <param name="loggerMode">The logger mode to enable.</param>
        public void Enable(LoggerMode loggerMode)
        {
            if (!ModesEnabling.TryAdd(loggerMode, true))
                ModesEnabling[loggerMode] = true;
        }

        /// <summary>
        /// Disables logging for a specific logger mode.
        /// </summary>
        /// <param name="loggerMode">The logger mode to disable.</param>
        public void Disable(LoggerMode loggerMode)
        {
            if (!ModesEnabling.TryAdd(loggerMode, false))
                ModesEnabling[loggerMode] = false;
        }

        /// <summary>
        /// Loads logger configuration from a file asynchronously and enables corresponding logger modes.
        /// </summary>
        private async Task<bool> FromFileAsync()
        {
            if (string.IsNullOrEmpty(LevelingFilePath) || !File.Exists(LevelingFilePath))
                return false;
            
            var enables = await HotIO.LoadPairsAsync(LevelingFilePath, "$");
            enables.Select(Parse).ToList().ForEach(Enable);
            return true;

            static LoggerMode Parse(KeyValuePair<string, string> config)
            {
                if (!int.TryParse(config.Value, out var level))
                    throw new Exception("Unable to parse logger config.");
                return new LoggerMode(config.Key, level);
            }
        }

        private void Manual()
        {
            Enable(LoggerMode.Essential());
            Enable(LoggerMode.Advanced());
            Enable(LoggerMode.Tracing());
            Enable(LoggerMode.Diagnostic());
            Enable(LoggerMode.Debugging());
        }

        /// <summary>
        /// Configures the logger by enabling the print-all mode and loading configuration from a file.
        /// </summary>
        protected virtual async void Configure()
        {
            Enable(PrintAllMode);
            if (!await FromFileAsync())
                Manual();
        }
        #endregion

        /// <summary>
        /// Determines whether the given logger mode should be printed based on the current configuration.
        /// </summary>
        /// <param name="loggerMode">The logger mode to evaluate.</param>
        /// <returns><see langword="true"/> if the logger mode should be printed; otherwise, <see langword="false"/>.</returns>
        public virtual bool ShouldPrint(LoggerMode loggerMode)
        {
            if (DeepToLevel > -1)
                return loggerMode.Level <= DeepToLevel;
            if (ModesEnabling.TryGetValue(loggerMode, out var @switch))
                return @switch;
            return false;
        }

        /// <summary>
        /// Logs the specified <see cref="LogEvent"/> if its logger mode should be printed, optionally localizing the message.
        /// </summary>
        /// <param name="log">The log event to log.</param>
        /// <param name="language">The language code for localizing the message. Defaults to English.</param>
        /// <returns><see langword="true"/> if the log was printed; otherwise, <see langword="false"/>.</returns>
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

        /// <summary>
        /// Logs a message with the specified log type and logger mode.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="logType">The log type (e.g., Error, Warning).</param>
        /// <param name="loggerMode">The logger mode. If null, the default or resolved mode is used.</param>
        public virtual void Log(LocalSet message, string logType, LoggerMode? loggerMode = null)
        {
            if (DeepToLevel < 0 && loggerMode is not null)
                loggerMode = Resolve(loggerMode.Name);

            var log = new LogEvent(message, logType, loggerMode ?? DefaultLoggerMode ?? PrintAllMode);
            Log(log);
        }

        /// <summary>
        /// Logs a string message with the specified log type and logger mode.
        /// </summary>
        /// <param name="message">The string message to log.</param>
        /// <param name="logType">The log type.</param>
        /// <param name="loggerMode">The logger mode.</param>
        public virtual void Log(string message, string logType = nameof(LogType.Message), LoggerMode? loggerMode = null) => Log((LocalSet)message, logType, loggerMode);

        /// <summary>
        /// Abstract method to drop empty logs. Must be implemented by derived classes.
        /// </summary>
        public abstract void DropEmpty();

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        /// <param name="loggerMode">The logger mode.</param>
        public virtual void Error(LocalSet message, LoggerMode? loggerMode = null) => Log(message, LogType.Error.ToString(), loggerMode);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        /// <param name="loggerMode">The logger mode.</param>
        public virtual void Warn(LocalSet message, LoggerMode? loggerMode = null) => Log(message, LogType.Warning.ToString(), loggerMode);

        /// <summary>
        /// Logs a success message.
        /// </summary>
        /// <param name="message">The success message to log.</param>
        /// <param name="loggerMode">The logger mode.</param>
        public virtual void Success(LocalSet message, LoggerMode? loggerMode = null) => Log(message, LogType.Successful.ToString(), loggerMode);

        /// <summary>
        /// Logs a system message.
        /// </summary>
        /// <param name="message">The system message to log.</param>
        /// <param name="loggerMode">The logger mode.</param>
        public virtual void System(LocalSet message, LoggerMode? loggerMode = null) => Log(message, LogType.System.ToString(), loggerMode);

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The informational message to log.</param>
        /// <param name="loggerMode">The logger mode.</param>
        public virtual void Inform(LocalSet message, LoggerMode? loggerMode = null) => Log(message, LogType.Information.ToString(), loggerMode);
    }
}