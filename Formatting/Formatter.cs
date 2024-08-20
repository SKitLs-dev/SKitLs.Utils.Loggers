using SKitLs.Utils.Localizations.Localizators;
using SKitLs.Utils.Loggers.Logging;
using SKitLs.Utils.Loggers.Prototype;

namespace SKitLs.Utils.Loggers.Formatting
{
    public class Formatter : IFormatter
    {
        public ILocalizator Localizator { get; set; }

        private List<(LogMatch Condition, LogFormat Formatter)> Rules { get; set; } = [];

        public void AddRule(LogMatch condition, LogFormat formatter)
        {
            Rules.Add((condition, formatter));
        }

        public virtual void Configure()
        {

        }

        public void Format(LogEvent log)
        {
            Rules.Where(x => x.Condition(log)).ToList().ForEach(rule => rule.Formatter(log));
        }
    }
}