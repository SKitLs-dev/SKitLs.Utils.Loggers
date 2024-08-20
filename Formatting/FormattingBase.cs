using SKitLs.Utils.Loggers.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKitLs.Utils.Loggers.Formatting
{
    public abstract class FormattingBase
    {
        public abstract bool ShouldFormat(LogEvent log);

        public abstract void Format(LogEvent log);
    }
}