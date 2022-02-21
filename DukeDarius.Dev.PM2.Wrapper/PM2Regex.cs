using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DukeDarius.Dev.PM2.Wrapper
{
    public static class PM2Regex
    {
        /// <summary>
        /// Group 1: Optional, WARN/ERR
        /// Group 2: Message
        /// </summary>
        public static readonly Regex PM2Message = new Regex(@"(?:\[PM2\])(\[[A-Z]*\])? (.*)");

        /// <summary>
        /// Group 1: PM2 Id
        /// Group 2: AppName
        /// Group 3: Namespace
        /// Group 4: Version
        /// Group 5: Mode
        /// Group 6: PID
        /// Group 7: Uptime
        /// Group 8: Restarts
        /// Group 9: Status
        /// Group 10: CPU Percent
        /// Group 11: RAM Usage
        /// Group 12: User
        /// </summary>
        public static readonly Regex PM2ProcessesPrintout = new Regex(@"\│(\s*\d*\s*)?\│(.*)?\│(.*)?\│(.*)?\│(.*)?\│(\s*\d*\s*)\│(.*)?\│(\s*\d*\s*)\│(.*)?\│(.*)?\│(.*)?\│(.*)?");
    }
}
