using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;

namespace G1ANT.Addon.MSOffice
{
    [Addon(Name = "MSOffice",
        Tooltip = "MSOffice Commands")]
    [CommandGroup(Name = "excel", Tooltip = "Excel commands ")]
    [CommandGroup(Name = "word", Tooltip = "Word commands")]
    [CommandGroup(Name = "outlook", Tooltip = "Outlook commands")]
    public class MSOfficeAddon : Language.Addon
    {
    }
}