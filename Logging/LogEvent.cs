using SKitLs.Utils.Localizations.Model;
using SKitLs.Utils.Loggers.Prototype;

namespace SKitLs.Utils.Loggers.Logging
{
    /// <summary>
    /// Represents a log event that encapsulates a message, its type, and associated logging mode.
    /// </summary>
    public class LogEvent
    {
        /// <summary>
        /// Indicates whether the log event timestamps should use UTC time. Defaults to <see langword="false"/>.
        /// </summary>
        public static bool UseUtc { get; set; }

        /// <summary>
        /// Gets or sets the time when the log event occurred.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the localized message associated with the log event.
        /// </summary>
        public LocalSet Message { get; set; }

        /// <summary>
        /// Gets or sets the type of the log event (e.g., Error, Warning, Information).
        /// </summary>
        public string LogType { get; set; }

        /// <summary>
        /// Gets or sets the logging mode used for this event.
        /// </summary>
        public LoggerMode LoggerMode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEvent"/> class with default values.
        /// </summary>
        public LogEvent() : this((LocalSet)"#empty", Prototype.LogType.Information.ToString(), LoggerBase.PrintAllMode) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEvent"/> class with the specified message, log type, and mode.
        /// </summary>
        /// <param name="message">The localized message associated with the log event.</param>
        /// <param name="logType">The type of log event (e.g., Error, Warning, Information).</param>
        /// <param name="mode">The logging mode used for this event.</param>
        public LogEvent(LocalSet message, string logType, LoggerMode mode)
        {
            Time = UseUtc ? DateTime.UtcNow : DateTime.Now;
            Message = message;
            LogType = logType;
            LoggerMode = mode;
        }

        /// <summary>
        /// Determines the <see cref="LogType"/> of the event based on its string representation.
        /// </summary>
        /// <returns>The log type of the event.</returns>
        public LogType Type()
        {
            if (Enum.TryParse<LogType>(LogType, out var type))
                return type;
            return Prototype.LogType.Other;
        }

        /// <summary>
        /// Determines if the log event is of type <see cref="Prototype.LogType.Message"/>.
        /// </summary>
        /// <returns><see langword="true"/> if the log event is a message; otherwise, <see langword="false"/>.</returns>
        public bool IsMessage() => Type() == Prototype.LogType.Message;

        /// <summary>
        /// Determines if the log event is of type <see cref="Prototype.LogType.Warning"/>.
        /// </summary>
        /// <returns><see langword="true"/> if the log event is a warning; otherwise, <see langword="false"/>.</returns>
        public bool IsWarning() => Type() == Prototype.LogType.Warning;

        /// <summary>
        /// Determines if the log event is of type <see cref="Prototype.LogType.Information"/>.
        /// </summary>
        /// <returns><see langword="true"/> if the log event is informational; otherwise, <see langword="false"/>.</returns>
        public bool IsInfo() => Type() == Prototype.LogType.Information;

        /// <summary>
        /// Determines if the log event is of type <see cref="Prototype.LogType.Error"/>.
        /// </summary>
        /// <returns><see langword="true"/> if the log event is an error; otherwise, <see langword="false"/>.</returns>
        public bool IsError() => Type() == Prototype.LogType.Error;

        /// <summary>
        /// Determines if the log event is of type <see cref="Prototype.LogType.Successful"/>.
        /// </summary>
        /// <returns><see langword="true"/> if the log event represents success; otherwise, <see langword="false"/>.</returns>
        public bool IsSuccess() => Type() == Prototype.LogType.Successful;

        /// <summary>
        /// Determines if the log event is of type <see cref="Prototype.LogType.System"/>.
        /// </summary>
        /// <returns><see langword="true"/> if the log event is a system event; otherwise, <see langword="false"/>.</returns>
        public bool IsSystem() => Type() == Prototype.LogType.System;

        /// <summary>
        /// Determines if the log event is of type <see cref="Prototype.LogType.Other"/>.
        /// </summary>
        /// <returns><see langword="true"/> if the log event is of another type not explicitly defined; otherwise, <see langword="false"/>.</returns>
        public bool IsOther() => Type() == Prototype.LogType.Other;

        /// <inheritdoc/>
        public override string ToString() => $"[{LogType}] {Message.LocalizationKey[..32]}...";
    }
}