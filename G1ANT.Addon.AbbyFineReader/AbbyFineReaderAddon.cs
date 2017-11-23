using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;

namespace G1ANT.Addon.Ocr.AbbyyFineReader
{
    [Addon(Name = "AbbyyFineReader",
        Tooltip = "AbbyyFineReader Commands")]
    [CommandGroup(Name = "ocrabbyy", Tooltip = "Abbyy Optical Character Recognition, uses Abbyy.")]
    public class AbbyyFineReaderAddon : Language.Addon
    {
    }
}