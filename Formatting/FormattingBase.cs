using SKitLs.Utils.Loggers.Logging;

namespace SKitLs.Utils.Loggers.Formatting
{
    /// <summary>
    /// The <see cref="FormattingBase"/> class is an abstract base class for custom formatters.
    /// Derived classes must implement the <see cref="ShouldFormat"/> and <see cref="Format"/> methods.
    /// </summary>
    public abstract class FormattingBase
    {
        /// <summary>
        /// Determines whether the specified log event should be formatted by this formatter.
        /// </summary>
        /// <param name="log">The log event to check.</param>
        /// <returns><see langword="true"/> if the log event should be formatted; otherwise, <see langword="false"/>.</returns>
        public abstract bool ShouldFormat(LogEvent log);

        /// <summary>
        /// Formats the specified log event according to the formatter's rules.
        /// </summary>
        /// <param name="log">The log event to format.</param>
        public abstract void Format(LogEvent log);
    }
}