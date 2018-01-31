﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;


namespace G1ANT.Addon.Ocr.AbbyyFineReader
{
    [Structure(Name = "abbydocument")]
    public class AbbyDocumentStructure : StructureTyped<CustomDocument>
    {
        private const string FirstIndex = "pages";
        private const string LastIndex = "count";
        

        public AbbyDocumentStructure(CustomDocument document) : this(document, null, null)
        {
            Value = document;
        }

        public override string ToString(string format = "")
        {
            return  Value.ToString();
        }
        public AbbyDocumentStructure(object value, string format = null, AbstractScripter scripter = null)
            : base(value, format, scripter)
        {
            Indexes.Add(FirstIndex);
            Indexes.Add(LastIndex);
        }

        public override Structure Get(string index = null)
        {
            switch (index)
            {
                case null:
                    return this;
                case "pages":
                    List<Structure> pages = new List<Structure>();
                    foreach (CustomPage page in Value.Pages)
                    {
                        pages.Add(new AbbyPageStructure(page));
                    }
                    return new ListStructure(pages);
                case "count":
                    return new IntegerStructure(Value.Pages.Count);
            }
            int intIndex = 0;
            if (int.TryParse(index, out intIndex) && intIndex < Value.Pages.Count && intIndex >= 0)
            {
                return new AbbyPageStructure(Value.Pages[intIndex]);
            }
            throw new ArgumentOutOfRangeException($"There is no value under index = {index}");
        }

        public override void Set(Structure value, string index = null)
        {
            throw new NotSupportedException("This value is read-only");
        }
    }
}
