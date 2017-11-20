using G1ANT.Language.Commands;
using G1ANT.Language.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Structures
{
    [Attributes.Structure(Name = "abbypage", Type = typeof(CustomPage), Order = int.MaxValue)]

    public class AbbyPage : Structure
    {
        public CustomPage Value { get; set; }

        public AbbyPage(CustomPage page)
        {
            Value = page;
        }

        public override bool Compare(Structure item)
        {
            return Value == ((AbbyPage)item).Value;
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
                case "rows":
                    List<Structure> rows = new List<Structure>();
                    foreach (CustomRow row in Value.Rows)
                    {
                        rows.Add(new AbbyRow(row));
                    }
                    return new List(rows);
                case "count":
                    return new Integer(Value.Rows.Count);
            }
            int intIndex = 0;
            if (int.TryParse(index, out intIndex) && intIndex < Value.Rows.Count && intIndex >= 0)
            {
                return new AbbyRow(Value.Rows[intIndex]);
            }
            throw new ArgumentOutOfRangeException($"There is no value under index = {index}");
        }

        public override void SetValue(Structure value, string index = null)
        {
            throw new NotSupportedException("This value is read-only");
        }

        public static AbbyPage Parse(object data, ILanguageParser parser)
        {
            return data as AbbyPage;
        }
    }
}
