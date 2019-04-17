using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.Net.Commands
{
    [Variable(
        Name = "timeoutmailsmtp",
        Tooltip = "Determines the timeout value (in ms) for the mail.smtp command; the default value is 10000 (10 seconds)")]
    public class TimeoutMailSmtpVariable : Variable
    {
        private TimeSpanStructure value;

        public TimeoutMailSmtpVariable(AbstractScripter scripter = null) : base(scripter)
        {
            value = new TimeSpanStructure(new TimeSpan(0, 0, 10), "", scripter);
        }

        public override Structure GetValue(string index = null)
        {
            return value.Get(index);
        }

        public override void SetValue(Structure value, string index = null)
        {
            this.value.Set(value, index);
        }
    }
}
