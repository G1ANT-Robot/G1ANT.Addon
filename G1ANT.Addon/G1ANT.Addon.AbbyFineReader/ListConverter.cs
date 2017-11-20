using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language.Structures;

namespace G1ANT.Language.Ocr.AbbyyFineReader
{
    public static class ListConverter
    {
        public static List<string> ExtractDictionary(List<Structure> value)
        {
            List<string> list = null;
            if (value != null)
            {
                list = new List<string>(value.Capacity);
                foreach (Structure item in value)
                {
                    list.Add(((Language.Structures.String)item).Value);
                }
            }
            return list;
        }
    }
}
