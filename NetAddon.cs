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
    [CommandGroup(Name = "as400", Icon = Properties.Resources.neticon, Tooltip = "A command used to work with IBM AS/400 platform.")]
    [CommandGroup(Name = "is", Icon = Properties.Resources.neticon, Tooltip = "A command used for checking sth.")]
    [CommandGroup(Name = "mail", Icon = Properties.Resources.neticon, Tooltip = "A command used with email")]
    [CommandGroup(Name = "rest", Icon = Properties.Resources.neticon, Tooltip = "This command prepares a request to the desired url with selected method.")]
    [CommandGroup(Name = "vnc", Icon = Properties.Resources.neticon, Tooltip = "A command connected with a VNC server.")]
    public class NetAddon : Language.Addon
    {
    }
}