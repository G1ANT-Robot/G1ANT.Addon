using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;

namespace G1ANT.Addon.Ocr
{
    [Addon(Name = "Ocr",
        Tooltip = "Ocr Commands")]
    [CommandGroup(Name = "ocr", Tooltip = "Command connected with creating, editing and generally working on ocr")]
    public class OcrAddon : Language.Addon
    {
    }
}