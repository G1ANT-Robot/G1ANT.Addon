using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Structures
{
    public class CustomPage
    {
        public List<CustomRow> Rows { get; set; } = new List<CustomRow>();

        public int Index { get; set; }
    }
}
