using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;

namespace G1ANT.Addon.IExplorer
{
    [Addon(Name = "IExplorer",
        Tooltip = "IExplorer Commands")]
    [CommandGroup(Name = "ie", Icon = Properties.Resources.ieicon, Tooltip = "Commands working with Internet Explorer.")]
    public class IExplorerAddon : Language.Addon
    {
    }
}