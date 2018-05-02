using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using G1ANT.Language;

namespace G1ANT.Addon.Xlsx
{
    [Command(Name = "xlsx.getcolumn", IsUnderConstruction = true)]
    public class XlsxGetColumnCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Column { get; set; }

            [Argument]
            public TextStructure RowSpan { get; set; } = new TextStructure(":");

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public XlsxGetColumnCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            List<object> column = XlsxManager.CurrentXlsx.GetColumn(arguments.RowSpan.Value, arguments.Column.Value);

            Scripter.Variables.SetVariableValue(arguments.Result.Value, new ListStructure(column, "", Scripter));
        }
    }
}
