namespace SKitLs.Utils.Loggers.Prototype
{
    /// <summary>
    /// <see cref="LogType"/> enum serves as a central component for categorizing different types of log messages within the project.
    /// By using this enumeration, you can easily identify and manage logs based on their specific purposes and significance.
    /// This enhances the overall organization of log data, making it more manageable and accessible.
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// Represents simple message type.
        /// Messages are just logged without any formatting.
        /// </summary>
        Message,

        /// <summary>
        /// Represents warning message type.
        /// Warnings should attract attention.
        /// </summary>
        Warning,

        /// <summary>
        /// Represents information message type.
        /// Should attract attention and inform about some event.
        /// </summary>
        Information,

        /// <summary>
        /// Represents error message type.
        /// Errors should inform that something went wrong.
        /// </summary>
        Error,

        /// <summary>
        /// Represents success message type
        /// Should be raised when an action was done successfully.
        /// </summary>
        Successful,

        /// <summary>
        /// Represents system message type.
        /// System messages can be referred to some internal events, which could simplify debugging process.
        /// </summary>
        System,

        /// <summary>
        /// Represents other (or custom) message type.
        /// </summary>
        Other,
    }
}