using SKitLs.Utils.Localizations.Localizators;
using SKitLs.Utils.Loggers.Logging;
using SKitLs.Utils.Loggers.Prototype;

namespace SKitLs.Utils.Loggers.Formatting
{
    /// <summary>
    /// The <see cref="Formatter"/> class implements the <see cref="IFormatter"/> interface.
    /// It provides a mechanism to format log events based on a set of rules.
    /// <para>
    /// Rules are added using <see cref="AddRule"/> and applied to log events using <see cref="Format"/>.
    /// </para>
    /// </summary>
    public class Formatter : IFormatter
    {
        /// <summary>
        /// Gets or sets the localizator used for localizing log messages.
        /// </summary>
        public ILocalizator Localizator { get; set; }

        /// <summary>
        /// The list of rules used for formatting log events. Each rule consists of a condition and a corresponding formatter.
        /// </summary>
        private List<(LogMatch Condition, LogFormat Formatter)> Rules { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="Formatter"/> class with the specified localizator.
        /// </summary>
        /// <param name="localizator">The localizator used for localizing log messages. Cannot be <see langword="null"/>.</param>
        public Formatter(ILocalizator localizator)
        {
            Localizator = localizator;
        }

        /// <summary>
        /// Adds a new formatting rule to the formatter.
        /// </summary>
        /// <param name="condition">The condition that determines when the rule should be applied.</param>
        /// <param name="formatter">The formatter to be applied if the condition is met.</param>
        public void AddRule(LogMatch condition, LogFormat formatter)
        {
            Rules.Add((condition, formatter));
        }

        /// <summary>
        /// Configures the formatter. This method is meant to be overridden by derived classes
        /// to set up additional formatting rules or configuration.
        /// </summary>
        public virtual void Configure()
        {
            // Default implementation is empty. Derived classes can override this method to add custom configuration.
        }

        /// <summary>
        /// Applies all applicable formatting rules to the provided log event.
        /// </summary>
        /// <param name="log">The log event to be formatted.</param>
        public void Format(LogEvent log)
        {
            Rules.Where(x => x.Condition(log)).ToList().ForEach(rule => rule.Formatter(log));
        }
    }
}