using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;

namespace G1ANT.Addon.Xls
{
    [Addon(Name = "Xls",
        Tooltip = "Xls Commands")]
    [CommandGroup(Name = "xls", Tooltip = "Command connected with creating, editing and generally working on xls")]

    public class XlsAddon : Language.Addon
    {
    }
}