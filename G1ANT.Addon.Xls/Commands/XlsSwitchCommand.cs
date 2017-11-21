using System;
using G1ANT.Language;

namespace G1ANT.Addon.Xls
{
    [Command(Name = "xls.switch", Tooltip = "This command allows to switch between open .xlsx files.")]
    public class XlsSwitchCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public IntegerStructure Id { get; set; } = new IntegerStructure(0);

            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");
        }
        public XlsSwitchCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                int id = arguments.Id.Value;
                bool result = XlsManager.SwitchXls(id);
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(result));
            }
            catch
            {
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(false));
                throw new ApplicationException("Specified Xls not existing");
            }
        }
    }
}
