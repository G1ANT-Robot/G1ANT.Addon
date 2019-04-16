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
        Tooltip = "Determines the timeout value (in ms) for several `ie.` commands; the default value is 20000 (20 seconds)")]
    public class TimeoutIEVariable : Variable
    {
        private TimeSpanStructure value;

        public TimeoutIEVariable(AbstractScripter scripter = null) : base(scripter)
        {
            value = new TimeSpanStructure(new TimeSpan(0, 0, 20), "", scripter);
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
