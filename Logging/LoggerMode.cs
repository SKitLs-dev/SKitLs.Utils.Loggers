namespace SKitLs.Utils.Loggers.Logging
{
    /// <summary>
    /// Represents a logging mode with a specific name and level, defining how detailed or verbose logging should be.
    /// </summary>
    public class LoggerMode : IEquatable<LoggerMode>
    {
        /// <summary>
        /// Gets or sets the name of the logger mode.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the logging level, where higher values indicate more detailed logging.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerMode"/> class with the specified name and logging level.
        /// </summary>
        /// <param name="name">The name of the logger mode.</param>
        /// <param name="level">The logging level associated with this mode.</param>
        public LoggerMode(string name, int level)
        {
            Name = name;
            Level = level;
        }

        /// <summary>
        /// Implicit conversion from <see cref="string"/> to <see cref="LoggerMode"/>. Creates a logger mode with a default level of 0.
        /// </summary>
        /// <param name="name">The name of the logger mode.</param>
        public static implicit operator LoggerMode(string name) => new(name, 0);

        /// <summary>
        /// Implicit conversion from <see cref="int"/> to <see cref="LoggerMode"/>. Creates a logger mode with a default name.
        /// </summary>
        /// <param name="level">The logging level.</param>
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

        #region Shortcuts
        /// <summary>
        /// Gets the essential logging mode, with a logging level of 1.
        /// </summary>
        /// <returns>A <see cref="LoggerMode"/> with the name "Essential" and a level of 1.</returns>
        public static LoggerMode Essential() => new("Essential", 1);

        /// <summary>
        /// Gets the advanced logging mode, with a logging level of 3.
        /// </summary>
        /// <returns>A <see cref="LoggerMode"/> with the name "Advanced" and a level of 3.</returns>
        public static LoggerMode Advanced() => new("Advanced", 3);

        /// <summary>
        /// Gets the tracing logging mode, with a logging level of 5.
        /// </summary>
        /// <returns>A <see cref="LoggerMode"/> with the name "Tracing" and a level of 5.</returns>
        public static LoggerMode Tracing() => new("Tracing", 5);

        /// <summary>
        /// Gets the diagnostic logging mode, with a logging level of 7.
        /// </summary>
        /// <returns>A <see cref="LoggerMode"/> with the name "Diagnostic" and a level of 7.</returns>
        public static LoggerMode Diagnostic() => new("Diagnostic", 7);

        /// <summary>
        /// Gets the debugging logging mode, with a logging level of 9.
        /// </summary>
        /// <returns>A <see cref="LoggerMode"/> with the name "Debugging" and a level of 9.</returns>
        public static LoggerMode Debugging() => new("Debugging", 9);
        #endregion
    }
}