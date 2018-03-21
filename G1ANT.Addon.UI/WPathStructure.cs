using G1ANT.Language;
using System;

namespace G1ANT.Addon.UI
{
    [Structure(Name = "wpath", Default = "", AutoCreate = false)]
    public class WPathStructure : StructureTyped<string>
    {
        public WPathStructure(string value, string format = "", AbstractScripter scripter = null) :
            base(value, format, scripter)
        {
            Init();
        }

        public WPathStructure(object value, string format = null, AbstractScripter scripter = null)
            : base(value, format, scripter)
        {
            Init();
        }

        protected void Init()
        {
        }

        public override Structure Get(string index = "")
        {
            if (string.IsNullOrWhiteSpace(index))
                return new WPathStructure(Value, Format);
            throw new ArgumentException($"Unknown index '{index}'");
        }

        public override void Set(Structure structure, string index = null)
        {
            if (structure == null || structure.Object == null)
                throw new ArgumentNullException(nameof(structure));
            else if (string.IsNullOrWhiteSpace(index))
                Value = structure.ToString();
            else
                throw new ArgumentException($"Unknown index '{index}'");
        }

        public override string ToString(string format)
        {
            return Value;
        }

        protected override string Parse(string value, string format = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            if (value.StartsWith(SpecialChars.Text) && value.EndsWith(SpecialChars.Text))
                return value.Substring(1).Substring(0, value.Length - 2);

            return value;
        }
    }
}
