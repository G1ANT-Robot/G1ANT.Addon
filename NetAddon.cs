using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;

namespace G1ANT.Addon.Net
{
    [Addon(Name = "Net",
        Tooltip = "Net Commands")]
    [CommandGroup(Name = "as400", Tooltip = "Command connected with creating, editing and generally working on excel")]
    [CommandGroup(Name = "is", Tooltip = "Command connected with creating, editing and generally working on excel")]
    [CommandGroup(Name = "mail", Tooltip = "Command connected with creating, editing and generally working on excel")]
    [CommandGroup(Name = "ping", Tooltip = "Command connected with creating, editing and generally working on excel")]
    [CommandGroup(Name = "rest", Tooltip = "Command connected with creating, editing and generally working on excel")]
    [CommandGroup(Name = "vnc", Tooltip = "Command connected with creating, editing and generally working on excel")]

    public class NetAddon : Language.Addon
    {
    }
}