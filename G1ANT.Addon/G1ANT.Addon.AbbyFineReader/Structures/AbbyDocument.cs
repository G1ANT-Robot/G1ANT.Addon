using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language.Structures;
using G1ANT.Language.Commands;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Structures
{
    [Attributes.Structure(Name = "abbydocument", Type = typeof(CustomDocument), Order = int.MaxValue)]
    public class AbbyDocument : G1ANT.Language.Structures.Structure
    {
        public CustomDocument Value { get; set; }

        public AbbyDocument(CustomDocument document)
        {
            Value = document;
        }

        public override bool Compare(Structure item)
        {
            return Value == ((AbbyDocument)item).Value;
        }

        public override Structure DeepCopy()
        {
            throw new NotSupportedException("Coping of abbyy objects is not alowed");
        }

        public override string ToString(bool typePrefix = false)
        {
            return (typePrefix ? Prefix : "") + Value.ToString();
        }

        public override Structure GetValue(string index = null)
        {
            switch (index)
            {
                case null:
                    return this;
                case "pages":
                    List<Structure> pages = new List<Structure>();
                    foreach (CustomPage page in Value.Pages)
                    {
                        pages.Add(new AbbyPage(page));
                    }
                    return new List(pages);
                case "count":
                    return new Integer(Value.Pages.Count);
            }
            int intIndex = 0;
            if (int.TryParse(index, out intIndex) && intIndex < Value.Pages.Count && intIndex >= 0)
            {
                return new AbbyPage(Value.Pages[intIndex]);
            }
            throw new ArgumentOutOfRangeException($"There is no value under index = {index}");
        }

        public override void SetValue(Structure value, string index = null)
        {
            throw new NotSupportedException("This value is read-only");
        }

        public static AbbyDocument Parse(object data, ILanguageParser parser)
        {
            return data as AbbyDocument;
        }
    }
}
