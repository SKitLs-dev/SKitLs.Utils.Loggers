using SKitLs.Utils.Localizations.Model;
using SKitLs.Utils.Loggers.Prototype;
using System.Linq;

namespace SKitLs.Utils.Loggers.Logging
{
    public class LogEvent
    {
        public static bool UseUtc { get; set; }

        public DateTime Time { get; set; }

        public LocalSet Message { get; set; }

        public string LogType { get; set; }

        public LoggerMode LoggerMode { get; set; }

        public LogEvent() : this((LocalSet)"#empty", Prototype.LogType.Information.ToString(), LoggerBase.PrintAllMode) { }

        public LogEvent(LocalSet message, string logType, LoggerMode mode)
        {
            Time = UseUtc ? DateTime.UtcNow : DateTime.Now;
            Message = message;
            LogType = logType;
            LoggerMode = mode;
        }

        public LogType Type()
        {
            if (Enum.TryParse<LogType>(LogType, out var type))
                return type;
            return Prototype.LogType.Other;
        }

        public bool IsMessage() => Type() == Prototype.LogType.Message;
        public bool IsWarning() => Type() == Prototype.LogType.Warning;
        public bool IsInfo() => Type() == Prototype.LogType.Information;
        public bool IsError() => Type() == Prototype.LogType.Error;
        public bool IsSuccess() => Type() == Prototype.LogType.Successful;
        public bool IsSystem() => Type() == Prototype.LogType.System;
        public bool IsOther() => Type() == Prototype.LogType.Other;

        public override string ToString() => $"[{LogType}] {Message.LocalizationKey[..32]}...";
    }
}