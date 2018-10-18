using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.IExplorer
{
    [Variable(
        Name = "timeoutie",
        Tooltip = "Determines the timeout value for the ie. commands; the default value is 6000 ms.")]
    public class TimeoutIEVariable : Variable
    {
        private TimeSpanStructure value;

        public TimeoutIEVariable(AbstractScripter scripter = null) : base(scripter)
        {
            value = new TimeSpanStructure(new TimeSpan(0, 0, 60), "", scripter);
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
