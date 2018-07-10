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
        Tooltip = "")]
    public class TimeoutRestVariable : Variable
    {
        private IntegerStructure value;

        public TimeoutRestVariable(AbstractScripter scripter = null) : base(scripter)
        {
            value = new IntegerStructure(5_000, "", scripter);
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
