using SKitLs.Utils.Loggers.Model;

namespace SKitLs.Utils.Loggers.Prototype
{
    /// <summary>
    /// <see cref="ILogger"/> interface provides a standardized mechanism for logging messages throughout the project.
    /// It abstracts the logging functionality and defines a set of methods that any logging implementation must support.
    /// <para>
    /// This decoupling of logging logic allows you to switch between different logging implementations
    /// without affecting the core functionality of the application.
    /// </para>
    /// <para>
    /// If working with Console Projects, see <see cref="DefaultConsoleLogger"/> for default realization.
    /// </para>
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs custom <paramref name="message"/> with provided formatting rule <paramref name="logType"/>.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="logType">Log type. Defines formatting rules.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Log(string message, LogType logType = LogType.Message, bool standsAlone = true);

        #region Shortcuts
        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.Error"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Error(string message, bool standsAlone = true);
        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.Warning"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Warn(string message, bool standsAlone = true);
        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.Successful"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Success(string message, bool standsAlone = true);
        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.System"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void System(string message, bool standsAlone = true);
        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.Information"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Inform(string message, bool standsAlone = true);
        #endregion

        /// <summary>
        /// Pushes new empty message block.
        /// </summary>
        public void DropEmpty();
    }
}