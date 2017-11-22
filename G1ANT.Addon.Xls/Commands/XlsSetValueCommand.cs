using System;
using G1ANT.Language;

namespace G1ANT.Addon.Xls
{
    [Command(Name = "xls.setvalue",Tooltip = "This command allows to set value in specified cell in .xlsx file")]
    public class XlsSetValueCommand : Command
    {
        public  class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Value { get; set; } = new TextStructure(string.Empty);

            [Argument(Required = true)]
            public TextStructure Position { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public XlsSetValueCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                XlsManager.CurrentXls.SetValue(arguments.Position.Value, arguments.Value.Value);
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(true));
            }
            catch (Exception e)
            {
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(false));
                throw new ApplicationException($"Error in set value: {e.Message}");
                
            }
        }
    }
}
