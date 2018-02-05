﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;

namespace G1ANT.Addon.Ocr.AbbyyFineReader
{
    public static class ListConverter
    {
        public static List<string> ExtractDictionary(List<Object> value)
        {
            List<string> list = null;
            if (value != null)
            {
                list = new List<string>(value.Capacity);
                foreach (Structure item in value)
                {
                    list.Add(((TextStructure)item).Value);
                }
            }
            return list;
        }
    }
}
