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
        public void Log(string message, LogType type = LogType.Message, bool standsAlone = true)
        {
            Console.ForegroundColor = type switch
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

        public void Error(string message, bool standsAlone = true) => Log($"{(standsAlone ? "[X] " : string.Empty)}{message}", LogType.Error, standsAlone);
        public void Warn(string message, bool standsAlone = true) => Log($"{(standsAlone ? "[!] " : string.Empty)}{message}", LogType.Warning, standsAlone);
        public void Success(string message, bool standsAlone = true) => Log($"{(standsAlone ? "[✓] " : string.Empty)}{message}", LogType.Successful, standsAlone);
        public void System(string message, bool standsAlone = true) => Log($"{(standsAlone ? "[>] " : string.Empty)}{message}", LogType.System, standsAlone);
        public void Inform(string message, bool standsAlone = true) => Log($"{(standsAlone ? "[>] " : string.Empty)}{message}", LogType.Information, standsAlone);
        public void DropEmpty() => Log(Environment.NewLine);
    }
}