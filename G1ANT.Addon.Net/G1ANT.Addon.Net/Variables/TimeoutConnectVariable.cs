using G1ANT.Language;
using System;

namespace G1ANT.Addon.Net.Commands
{
    [Variable(
        Name = "timeoutconnect",
        Tooltip = "Determines the timeout value (in ms) for the is.accessible and ping commands; the default value is 1000 (1 second)")]
    public class TimeoutConnectVariable : Variable
    {
        private TimeSpanStructure value;

        public TimeoutConnectVariable(AbstractScripter scripter = null) : base(scripter)
        {
            value = new TimeSpanStructure(new TimeSpan(0, 0, 1), "", scripter);
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
