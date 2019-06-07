using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.Net.Commands
{
    [Variable(
        Name = "timeoutrest",
        Tooltip = "Determines the timeout value (in ms) for the rest command; the default value is 5000 (5 seconds)")]
    public class TimeoutRestVariable : Variable
    {
        private TimeSpanStructure value;

        public TimeoutRestVariable(AbstractScripter scripter = null) : base(scripter)
        {
            value = new TimeSpanStructure(new TimeSpan(0, 0, 5), "", scripter);
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
