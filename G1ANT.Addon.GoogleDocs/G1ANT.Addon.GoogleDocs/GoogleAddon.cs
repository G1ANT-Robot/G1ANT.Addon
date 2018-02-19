/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.GoogleDocs
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
using System.Drawing;

namespace G1ANT.Addon.GoogleDocs
{
    [Addon(Name = "GoogleDocs",
        Tooltip = "GoogleDocs Commands")]
    [CommandGroup( Name = "googlesheet", Tooltip = "Command connected with creating, editing and generally working on GoogleDocs")]
    [Copyright(Author = "G1ANT LTD", Copyright = "G1ANT LTD", Email = "hi@g1ant.com", Website = "www.g1ant.com")]
    [License(Type = "LGPL", ResourceName = "License.txt")]
    public class GoogleAddon : Language.Addon
    {
    }
}