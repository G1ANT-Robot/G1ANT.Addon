using System;

using G1ANT.Language.Commands;
using G1ANT.Language.Structures;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Structures
{
    [Attributes.Structure(Name = "abbycell", Type = typeof(CustomCell), Order = int.MaxValue)]

    public class AbbyCell : Structure
    {
        public CustomCell Value { get; set; }

        public AbbyCell(CustomCell cell)
        {
            Value = cell;
        }

        public override bool Compare(Structure item)
        {
            return Value == ((AbbyCell)item).Value;
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
                case "top":
                    return new Integer(Value.Top);
                case "bottom":
                    return new Integer(Value.Bottom);
                case "left":
                    return new Integer(Value.Left);
                case "right":
                    return new Integer(Value.Right);
                case "baseline":
                    return new Integer(Value.BaseLine);
                case "text":
                    return new Language.Structures.String(Value.Text);
            }
            throw new ArgumentOutOfRangeException($"There is no value under index = {index}");
        }

        public override void SetValue(Structure value, string index = null)
        {
            throw new NotSupportedException("This value is read-only");
        }

        public static AbbyCell Parse(object data, ILanguageParser parser)
        {
            return data as AbbyCell;
        }
    }
}
