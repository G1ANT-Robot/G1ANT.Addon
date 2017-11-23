using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;

namespace G1ANT.Addon.Watson
{
    [Addon(Name = "watson",
        Tooltip = "watson Commands")]
    [CommandGroup(Name = "watson", Tooltip = "Command connected with creating, editing and generally working on watson")]
    public class WatsonAddon : Language.Addon
    {
    }
}