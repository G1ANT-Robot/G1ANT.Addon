/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
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
    [Copyright(Author = "G1ANT LTD", Copyright = "G1ANT LTD", Email = "hi@g1ant.com", Website = "www.g1ant.com")]
    [License(Type = "LGPL", ResourceName = "License.txt")]
    [CommandGroup(Name = "as400", Tooltip = "A command used to work with IBM AS/400 platform.")]
    [CommandGroup(Name = "is", Tooltip = "A command used for checking sth.")]
    [CommandGroup(Name = "mail",  Tooltip = "A command used with email")]
    [CommandGroup(Name = "rest",  Tooltip = "This command prepares a request to the desired url with selected method.")]
    [CommandGroup(Name = "vnc",  Tooltip = "A command connected with a VNC server.")]
    public class NetAddon : Language.Addon
    {
    }
}