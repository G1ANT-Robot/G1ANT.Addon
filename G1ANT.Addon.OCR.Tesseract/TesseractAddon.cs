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
    [CommandGroup(Name = "ocroffline",  Tooltip = "Tesseract based optical character recognition, does not need internet connection.")]
    public class TesseractAddon : Language.Addon
    {
    }
}