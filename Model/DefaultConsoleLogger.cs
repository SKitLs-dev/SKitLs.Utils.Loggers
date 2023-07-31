using SKitLs.Utils.Loggers.Prototype;

namespace SKitLs.Utils.Loggers.Model
{
    /// <summary>
    /// <see cref="DefaultConsoleLogger"/> class is a concrete implementation of the <see cref="ILogger"/> interface,
    /// specifically tailored for Console Projects.
    /// It offers a default logging behavior for outputting events and messages to the console.
    /// <para>
    /// By leveraging this implementation, you can quickly enable logging in your Console-based applications
    /// without the need for extensive configuration.
    /// </para>
    /// </summary>
    public class DefaultConsoleLogger : ILogger
    {
        /// <summary>
        /// Logs custom <paramref name="message"/> with provided formatting rule <paramref name="logType"/>.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="logType">Log type. Defines formatting rules.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Log(string message, LogType logType = LogType.Message, bool standsAlone = true)
        {
            Console.ForegroundColor = logType switch
            {
                LogType.Message => ConsoleColor.White,
                LogType.Warning => ConsoleColor.Yellow,
                LogType.Error => ConsoleColor.Red,
                LogType.Successful => ConsoleColor.Green,
                LogType.Information => ConsoleColor.Cyan,
                _ => ConsoleColor.Gray,
            };
            if (standsAlone) Console.WriteLine(message);
            else Console.Write($"{message} ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.Error"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Error(string message, bool standsAlone = true) => Log($"{(standsAlone ? "[X] " : string.Empty)}{message}", LogType.Error, standsAlone);
        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.Warning"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Warn(string message, bool standsAlone = true) => Log($"{(standsAlone ? "[!] " : string.Empty)}{message}", LogType.Warning, standsAlone);
        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.Successful"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Success(string message, bool standsAlone = true) => Log($"{(standsAlone ? "[✓] " : string.Empty)}{message}", LogType.Successful, standsAlone);
        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.System"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void System(string message, bool standsAlone = true) => Log($"{(standsAlone ? "[>] " : string.Empty)}{message}", LogType.System, standsAlone);
        /// <summary>
        /// Logs <paramref name="message"/> as <see cref="LogType.Information"/> one.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="standsAlone">Determines whether <paramref name="message"/> should be logged as a stand-alone one
        /// (ex: NewLine for Console).</param>
        public void Inform(string message, bool standsAlone = true) => Log($"{(standsAlone ? "[>] " : string.Empty)}{message}", LogType.Information, standsAlone);
        /// <summary>
        /// Pushes new empty message block.
        /// </summary>
        public void DropEmpty() => Log(Environment.NewLine);
    }
}