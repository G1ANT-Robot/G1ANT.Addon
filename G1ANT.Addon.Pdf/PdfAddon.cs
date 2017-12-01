using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;

namespace G1ANT.Addon.Pdf
{
    [Addon(Name = "Pdf",
        Tooltip = "Pdf Commands")]
    [CommandGroup(Name = "pdf", Tooltip = "Addon to work with PDF files")]
    public class PdfAddon : Language.Addon
    {
    }
}
