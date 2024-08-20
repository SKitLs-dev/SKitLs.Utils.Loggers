using SKitLs.Utils.Loggers.Logging;

namespace SKitLs.Utils.Loggers.Prototype
{
    public delegate bool LogMatch(LogEvent log);
    public delegate void LogFormat(LogEvent log);

    public interface IFormatter
    {
        public void Format(LogEvent log);

        public void AddRule(LogMatch condition, LogFormat formatter);
    }
}