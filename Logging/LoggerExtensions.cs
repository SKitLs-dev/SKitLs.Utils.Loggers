using SKitLs.Utils.Localizations.Model;
using SKitLs.Utils.Loggers.Prototype;
using System.Runtime.CompilerServices;

namespace SKitLs.Utils.Loggers.Logging
{
    /// <summary>
    /// Provides extension methods for the <see cref="ILogger"/> interface to log various types of events and trace method calls.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Logs a system message with debugging details using the <see cref="LoggerMode.Debugging"/> level.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="message">The message to be logged.</param>
        public static void Debug(this ILogger logger, LocalSet message)
        {
            logger.System(message, LoggerMode.Debugging());
        }

        /// <summary>
        /// Logs a "Trace In" message for entering a method, including caller information such as member name, file path, and line number.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="mode">Optional. The logging mode to use. Defaults to <see cref="LoggerMode.Tracing"/>.</param>
        /// <param name="memberName">The name of the calling method. Automatically populated using the <see langword="CallerMemberName"/> attribute.</param>
        /// <param name="filePath">The file path of the calling method. Automatically populated using the <see langword="CallerFilePath"/> attribute.</param>
        /// <param name="lineNumber">The line number of the calling method. Automatically populated using the <see langword="CallerLineNumber"/> attribute.</param>
        public static void TraceIn(this ILogger logger, LoggerMode? mode = null, [CallerMemberName] string? memberName = null, [CallerFilePath] string? filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            mode ??= LoggerMode.Tracing();
            var className = Path.GetFileNameWithoutExtension(filePath);
            logger.System(new LocalSet("logging.TraceIn", [className, memberName, lineNumber, Path.GetFileName(filePath) ]), mode);
        }

        /// <summary>
        /// Logs a "Trace Out" message for exiting a method, including caller information such as member name, file path, and line number.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="mode">Optional. The logging mode to use. Defaults to <see cref="LoggerMode.Tracing"/>.</param>
        /// <param name="memberName">The name of the calling method. Automatically populated using the <see langword="CallerMemberName"/> attribute.</param>
        /// <param name="filePath">The file path of the calling method. Automatically populated using the <see langword="CallerFilePath"/> attribute.</param>
        /// <param name="lineNumber">The line number of the calling method. Automatically populated using the <see langword="CallerLineNumber"/> attribute.</param>
        public static void TraceOut(this ILogger logger, LoggerMode? mode = null, [CallerMemberName] string? memberName = null, [CallerFilePath] string? filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            mode ??= LoggerMode.Tracing();
            var className = Path.GetFileNameWithoutExtension(filePath);
            logger.System(new LocalSet("logging.TraceOut", [className, memberName, lineNumber, Path.GetFileName(filePath)]), mode);
        }

        /// <summary>
        /// Logs an exception message at the error level, using the provided <paramref name="logType"/>.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="logType">The log type to apply. Defaults to <see cref="LogType.Error"/>.</param>
        /// <param name="mode">Optional. The logging mode to use. Defaults to 1 (Essential).</param>
        public static void Exception(this ILogger logger, Exception ex, string logType = nameof(LogType.Error), LoggerMode? mode = null)
        {
            mode ??= 1;
            logger.Log(ex.Message, logType, mode);
        }

        /// <summary>
        /// Logs an exception with its full stack trace and inner exceptions, if any, using the provided <paramref name="logType"/>.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="logType">The log type to apply. Defaults to <see cref="LogType.Error"/>.</param>
        /// <param name="mode">Optional. The logging mode to use. Defaults to 1 (Essential).</param>
        public static void ExceptionFull(this ILogger logger, Exception ex, string logType = nameof(LogType.Error), LoggerMode? mode = null)
        {
            mode ??= 1;
            logger.Log($"Message: {ex.Message}\nStack trace: {ex.StackTrace}", logType, mode);
            if (ex.InnerException is not null)
            {
                logger.Log("Inner exception:", logType, mode);
                logger.ExceptionFull(ex.InnerException, logType, mode);
            }
        }
    }
}