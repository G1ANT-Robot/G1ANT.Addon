using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;

namespace G1ANT.Addon.Mscrm
{
    [Addon(Name = "mscrm",
        Tooltip = "MsCrm Commands")]
    [CommandGroup(Name = "mscrm", Icon = Properties.Resources.crmicon, Tooltip = "Command connected with creating, editing and generally working on mscrm")]
    public class MsCrmAddon : Language.Addon
    {
    }
}