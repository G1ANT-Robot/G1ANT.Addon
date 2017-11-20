using System;
using G1ANT.Language;

namespace G1ANT.Language.Xls
{
    [Command(Name = "xls.getvalue",Tooltip = "This command allows to get value of specified cell in .xlsx file")]
    public class GetCellValueCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Position { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");
        }
        public GetCellValueCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public  void Execute(Arguments arguments)
        {
            try
            {
                string returVariableName = arguments.Result.Value;
                // if (XlsManager.CurrentXls.GetValue(arguments.Position.Value)!= "")
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(XlsManager.CurrentXls.GetValue(arguments.Position.Value)));
            }
            catch
            {
                throw new ApplicationException("Error in get value");
            }
        }
    }
}
