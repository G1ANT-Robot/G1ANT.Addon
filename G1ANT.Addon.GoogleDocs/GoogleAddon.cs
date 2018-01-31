using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;
using System.Drawing;

namespace G1ANT.Addon.GoogleDocs
{
    [Addon(Name = "GoogleDocs",
        Tooltip = "GoogleDocs Commands")]
    [CommandGroup( Name = "googlesheet", Tooltip = "Command connected with creating, editing and generally working on GoogleDocs")]
    public class GoogleAddon : Language.Addon
    {
    }
}