﻿/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Watson
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

namespace G1ANT.Addon.Watson
{
    [Addon(Name = "watson",
        Tooltip = "watson Commands")]
    [CommandGroup(Name = "watson", Tooltip = "Command connected with creating, editing and generally working on watson")]
    public class WatsonAddon : Language.Addon
    {
    }
}