using G1ANT.Language.Commands;
using G1ANT.Language.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Structures
{
    [G1ANT.Language.Attributes.Structure(Name = "abbyrow", Type = typeof(CustomRow), Order = 999)]

    public class AbbyRow : Structure
    {
        public CustomRow Value { get; set; }

        public AbbyRow(CustomRow row)
        {
            Value = row;
        }

        public override bool Compare(Structure item)
        {
            return Value == ((AbbyRow)item).Value;
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
                case "cells":
                    return this;
                case "count":
                    return new Integer(Value.Cells.Count);
                case "top":
                    return new Integer(Value.Top);
                case "bottom":
                    return new Integer(Value.Bottom);
                case "istablerow":
                    return new Bool(Value.IsTableRow);
            }
            int intIndex = 0;
            if (int.TryParse(index, out intIndex) && intIndex < Value.Cells.Count && intIndex >= 0)
            {
                return new AbbyCell(Value.Cells[intIndex]);
            }
            throw new ArgumentOutOfRangeException($"There is no value under index = {index}");
        }

        public override void SetValue(Structure value, string index = null)
        {
            throw new NotSupportedException("This value is read-only");
        }

        public static AbbyRow Parse(object data, ILanguageParser parser)
        {
            return data as AbbyRow;
        }
    }
}
