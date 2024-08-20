using SKitLs.Utils.Localizations.Model;
using SKitLs.Utils.Loggers.Prototype;
using System.Runtime.CompilerServices;

namespace SKitLs.Utils.Loggers.Logging
{
    public static class LoggerExtensions
    {
        public static void TraceIn(this ILogger logger, LoggerMode? mode = null, [CallerMemberName] string? memberName = null, [CallerFilePath] string? filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            mode ??= LoggerMode.Tracing();
            var className = Path.GetFileNameWithoutExtension(filePath);
            logger.System(new LocalSet("logging.TraceIn", [className, memberName, lineNumber, Path.GetFileName(filePath) ]), mode);
        }

        public static void TraceOut(this ILogger logger, LoggerMode? mode = null, [CallerMemberName] string? memberName = null, [CallerFilePath] string? filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            mode ??= LoggerMode.Tracing();
            var className = Path.GetFileNameWithoutExtension(filePath);
            logger.System(new LocalSet("logging.TraceOut", [className, memberName, lineNumber, Path.GetFileName(filePath)]), mode);
        }
    }
}