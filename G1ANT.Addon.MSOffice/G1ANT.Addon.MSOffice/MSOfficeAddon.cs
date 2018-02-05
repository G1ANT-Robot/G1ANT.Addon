using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;
using System.Drawing;

namespace G1ANT.Addon.MSOffice
{
    [Addon(Name = "MSOffice",
        Tooltip = "MSOffice Commands")]
    [CommandGroup(Name = "excel", Tooltip = "Command connected with creating, editing and generally working on excel")]
    [CommandGroup(Name = "word",  Tooltip = "Command connected with creating, editing and generally working on word")]
    [CommandGroup(Name = "outlook", Tooltip = "Command connected with creating, editing and generally working on outlook")]
    public class MSOfficeAddon : Language.Addon
    {
    }
}