using SKitLs.Utils.Extensions.Lists;
using SKitLs.Utils.Localizations.Languages;
using SKitLs.Utils.Localizations.Localizators;
using SKitLs.Utils.Loggers.Prototype;

namespace SKitLs.Utils.Loggers.Logging
{
    /// <summary>
    /// <see cref="ConsoleLogger"/> class is a concrete implementation of the <see cref="ILogger"/> interface,
    /// specifically tailored for Console Projects.
    /// It offers a default logging behavior for outputting events and messages to the console.
    /// <para>
    /// By leveraging this implementation, you can quickly enable logging in your Console-based applications
    /// without the need for extensive configuration.
    /// </para>
    /// </summary>
    public class ConsoleLogger : LoggerBase
    {
        /// <summary>
        /// Gets or sets the maximum length of the log message output to the console.
        /// Any log message exceeding this length will be truncated.
        /// </summary>
        public int MaxOutputLength { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger"/> class with optional localization, formatting, and maximum output length.
        /// </summary>
        /// <param name="localizator">Optional localizator to localize log messages.</param>
        /// <param name="formatter">Optional formatter to format log messages. Defaults to a new <see cref="Formatting.Formatter"/> if null.</param>
        /// <param name="maxOutputLength">The maximum length of the log message to output to the console. Defaults to 128 characters.</param>
        public ConsoleLogger(ILocalizator? localizator = null, IFormatter? formatter = null, int maxOutputLength = 128) : base(localizator, formatter)
        {
            MaxOutputLength = maxOutputLength;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Configures the console logger by adding formatting rules specific to different log types (e.g., message, warning, error).
        /// </remarks>
        protected override void Configure()
        {
            base.Configure();
            Formatter.AddRule(log => log.IsMessage(), log => log.Message = $"[>] {log.Message}");
            Formatter.AddRule(log => log.IsSystem(), log => log.Message = $"[*] {log.Message}");
            Formatter.AddRule(log => log.IsInfo(), log => log.Message = $"[>] {log.Message}");
            Formatter.AddRule(log => log.IsOther(), log => log.Message = $"[>] {log.Message}");
            Formatter.AddRule(log => log.IsSuccess(), log => log.Message = $"[O] {log.Message}");
            Formatter.AddRule(log => log.IsWarning(), log => log.Message = $"[!] {log.Message}");
            Formatter.AddRule(log => log.IsError(), log => log.Message = $"[X] {log.Message}");
        }

        /// <inheritdoc/>
        public override bool Log(LogEvent log, LanguageCode language = LanguageCode.EN)
        {
            if (base.Log(log, language))
            {
                try
                {
                    Console.ForegroundColor = log.Type() switch
                    {
                        LogType.Message => ConsoleColor.Gray,
                        LogType.Warning => ConsoleColor.Yellow,
                        LogType.Error => ConsoleColor.Red,
                        LogType.Successful => ConsoleColor.Green,
                        LogType.Information => ConsoleColor.Cyan,
                        _ => ConsoleColor.DarkGray,
                    };
                    Console.WriteLine(log.Message.LocalizationKey.Take(MaxOutputLength).JoinString(string.Empty));
                }
                catch (Exception) { }
                finally
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public override void DropEmpty() => Log(Environment.NewLine);
    }
}