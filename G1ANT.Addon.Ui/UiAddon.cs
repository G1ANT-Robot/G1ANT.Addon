using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;

namespace G1ANT.Addon.Ui
{
    [Addon(Name = "Ui",
        Tooltip = "ui Commands")]
    [CommandGroup(Name = "ui", Tooltip = "Command connected with ui")]

    public class UiAddon : Language.Addon
    {
    }
}