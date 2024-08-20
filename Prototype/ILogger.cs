using SKitLs.Utils.Localizations.Languages;
using SKitLs.Utils.Localizations.Localizators;
using SKitLs.Utils.Localizations.Model;
using SKitLs.Utils.Loggers.Logging;

namespace SKitLs.Utils.Loggers.Prototype
{
    /// <summary>
    /// <see cref="ILogger"/> interface provides a standardized mechanism for logging messages throughout the project.
    /// It abstracts the logging functionality and defines a set of methods that any logging implementation must support.
    /// <para/>
    /// This decoupling of logging logic allows you to switch between different logging implementations
    /// without affecting the core functionality of the application.
    /// <para/>
    /// If working with Console Projects, see <see cref="ConsoleLogger"/> for default realization.
    /// </summary>
    public interface ILogger
    {
        public ILocalizator? Localizator { get; }

        public void UseLeveling(int maxDeepness);

        public void Enable(LoggerMode loggerMode);

        public void Disable(LoggerMode loggerMode);

        public bool Log(LogEvent log, LanguageCode language = LanguageCode.EN);

        public void Log(LocalSet message, string logType = nameof(LogType.Message), LoggerMode? loggerMode = null);

        /// <summary>
        /// Logs custom <paramref name="message"/> with provided formatting rule <paramref name="logType"/>.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="logType">Log type. Defines formatting rules.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Log(string message, string logType = nameof(LogType.Message), LoggerMode? loggerMode = null);

        #region Shortcuts
        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.Error"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Error(LocalSet message, LoggerMode? loggerMode = null);

        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.Warning"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Warn(LocalSet message, LoggerMode? loggerMode = null);

        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.Successful"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Success(LocalSet message, LoggerMode? loggerMode = null);

        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.System"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void System(LocalSet message, LoggerMode? loggerMode = null);

        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.Information"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Inform(LocalSet message, LoggerMode? loggerMode = null);
        #endregion

        /// <summary>
        /// Pushes new empty message block.
        /// </summary>
        public void DropEmpty();
    }
}