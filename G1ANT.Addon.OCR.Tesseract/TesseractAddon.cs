using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;

namespace G1ANT.Addon.Ocr.Tesseract
{
    [Addon(Name = "Tesseract",
        Tooltip = "Tesseract Commands")]
    [CommandGroup(Name = "ocroffline", Tooltip = "Command connected with creating, editing and generally working on ocroffline")]

    public class TesseractAddon : Language.Addon
    {
    }
}