using SKitLs.Utils.Loggers.Logging;

namespace SKitLs.Utils.Loggers.Prototype
{
    /// <summary>
    /// Delegate for determining whether a log event matches specific criteria.
    /// </summary>
    /// <param name="log">The log event to be evaluated.</param>
    /// <returns>Returns <see langword="true"/> if the log matches the criteria, <see langword="false"/> otherwise.</returns>
    public delegate bool LogMatch(LogEvent log);

    /// <summary>
    /// Delegate for formatting a log event.
    /// </summary>
    /// <param name="log">The log event to be formatted.</param>
    public delegate void LogFormat(LogEvent log);

    /// <summary>
    /// Defines methods for formatting log events and adding custom formatting rules.
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Formats a log event according to predefined rules.
        /// </summary>
        /// <param name="log">The log event to format.</param>
        public void Format(LogEvent log);

        /// <summary>
        /// Adds a custom formatting rule by defining a condition and a corresponding formatter.
        /// </summary>
        /// <param name="condition">A <see cref="LogMatch"/> delegate that specifies when the formatting rule should be applied.</param>
        /// <param name="formatter">A <see cref="LogFormat"/> delegate that defines how the log event should be formatted when the condition is met.</param>
        public void AddRule(LogMatch condition, LogFormat formatter);
    }
}