using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;

namespace G1ANT.Addon.Xlsx
{
    [Addon(Name = "Xlsx",
        Tooltip = "Xlsx Commands")]
    [CommandGroup(Name = "xlsx", Tooltip = "Command connected with creating, editing and generally working on xlsx")]

    public class XlsxAddon : Language.Addon
    {
    }
}