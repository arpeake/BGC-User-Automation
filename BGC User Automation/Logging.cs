using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC_User_Automation
{
    public class Logging
    {
        public enum LogType
        {
            None,
            Error,
            Warning,
            Critical,
            Message,
            Success
        };
    }
}
