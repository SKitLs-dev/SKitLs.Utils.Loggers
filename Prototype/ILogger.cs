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
    /// If working with Console Projects, see <see cref="ConsoleLogger"/> for a default implementation.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Gets the current <see cref="ILocalizator"/> instance used for localizing log messages.
        /// </summary>
        public ILocalizator? Localizator { get; }

        /// <summary>
        /// Sets the maximum depth level for logging.
        /// </summary>
        /// <param name="maxDeepness">The maximum deepness for log messages to be categorized.</param>
        public void UseLeveling(int maxDeepness);

        /// <summary>
        /// Enables the logger mode based on the specified <paramref name="loggerMode"/>.
        /// </summary>
        /// <param name="loggerMode">The mode of logging to enable.</param>
        public void Enable(LoggerMode loggerMode);

        /// <summary>
        /// Disables the logger mode based on the specified <paramref name="loggerMode"/>.
        /// </summary>
        /// <param name="loggerMode">The mode of logging to disable.</param>
        public void Disable(LoggerMode loggerMode);

        /// <summary>
        /// Logs an event with optional language localization.
        /// </summary>
        /// <param name="log">The event to be logged.</param>
        /// <param name="language">The language code for localization. Defaults to English.</param>
        /// <returns>Returns <see langword="true"/> if logging was successful, <see langword="false"/> otherwise.</returns>
        public bool Log(LogEvent log, LanguageCode language = LanguageCode.EN);

        /// <summary>
        /// Logs a localized message with a specified log type and optional logger mode.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="logType">Defines the type of log (e.g., error, info) and its formatting.</param>
        /// <param name="loggerMode">Optional logger mode for controlling the output.</param>
        public void Log(LocalSet message, string logType = nameof(LogType.Message), LoggerMode? loggerMode = null);

        /// <summary>
        /// Logs a custom message with the provided formatting rule <paramref name="logType"/>.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="logType">Defines formatting rules for the log (e.g., Error, Information).</param>
        /// <param name="loggerMode">Optional logger mode to control how the message is logged.</param>
        public void Log(string message, string logType = nameof(LogType.Message), LoggerMode? loggerMode = null);

        #region Shortcuts
        /// <summary>
        /// Logs a message as an <see cref="LogType.Error"/>.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggerMode">Optional logger mode for controlling the output.</param>
        public void Error(LocalSet message, LoggerMode? loggerMode = null);

        /// <summary>
        /// Logs a message as a <see cref="LogType.Warning"/>.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggerMode">Optional logger mode for controlling the output.</param>
        public void Warn(LocalSet message, LoggerMode? loggerMode = null);

        /// <summary>
        /// Logs a message as a <see cref="LogType.Successful"/>.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggerMode">Optional logger mode for controlling the output.</param>
        public void Success(LocalSet message, LoggerMode? loggerMode = null);

        /// <summary>
        /// Logs a message as a <see cref="LogType.System"/>.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggerMode">Optional logger mode for controlling the output.</param>
        public void System(LocalSet message, LoggerMode? loggerMode = null);

        /// <summary>
        /// Logs a message as <see cref="LogType.Information"/>.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggerMode">Optional logger mode for controlling the output.</param>
        public void Inform(LocalSet message, LoggerMode? loggerMode = null);
        #endregion

        /// <summary>
        /// Pushes a new empty message block for separation in logs.
        /// </summary>
        public void DropEmpty();
    }
}