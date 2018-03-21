﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;

namespace G1ANT.Addon.UI
{
    [Addon(Name = "UI", Tooltip = "Addon which support desktop application automations")]
    [Copyright(Author = "G1ANT LTD", Copyright = "G1ANT LTD", Email = "hi@g1ant.com", Website = "www.g1ant.com")]
    [License(Type = "LGPL", ResourceName = "License.txt")]
    [CommandGroup(Name = "ui", Tooltip = "Command allows desktop applications automation")]
    public class MainAddon : Language.Addon
    {
    }
}
