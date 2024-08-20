namespace SKitLs.Utils.Loggers.Logging
{
    public class LoggerMode : IEquatable<LoggerMode>
    {
        public string Name { get; set; }

        public int Level { get; set; }

        public LoggerMode(string name, int level)
        {
            Name = name;
            Level = level;
        }

        public static implicit operator LoggerMode(string name) => new(name, 0);

        public static implicit operator LoggerMode(int level) => new(LoggerBase.PrintAllModeName, level);

        /// <inheritdoc/>
        public override int GetHashCode() => Name.GetHashCode() * Level.GetHashCode();

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is LoggerMode mode)
                return Equals(mode);
            return base.Equals(obj);
        }

        /// <inheritdoc/>
        public bool Equals(LoggerMode? other)
        {
            if (Name.Equals(other?.Name))
                return true;
            return false;
        }

        /// <inheritdoc/>
        public override string ToString() => $"{Name} ({Level})";

        public static LoggerMode Essential() => new("Essential", 1);
        public static LoggerMode Advanced() => new ("Advanced", 3);
        public static LoggerMode Tracing() => new ("Tracing", 5);
        public static LoggerMode Diagnostic() => new ("Diagnostic", 7);
        public static LoggerMode Debugging() => new ("Debugging", 9);
    }
}