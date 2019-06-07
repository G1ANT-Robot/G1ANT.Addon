using G1ANT.Language;
using System;

namespace G1ANT.Addon.Watson
{
    [Variable(
        Name = "timeoutwatson",
        Tooltip = "Determines the timeout value (in ms) for the `watson.` commands; the default value is 60000 (60 seconds)")]
    public class TimeoutWatsonVariable : Variable
    {
        private readonly TimeSpanStructure value;
        public TimeoutWatsonVariable(AbstractScripter scripter = null) : base(scripter)
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
