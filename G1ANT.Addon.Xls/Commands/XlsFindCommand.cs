using System;
using G1ANT.Language;

namespace G1ANT.Addon.Xls
{
    [Command(Name = "xls.find",Tooltip = "This command allows to find address of a cell where specified value is stored.")]
    public class XlsFindCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Value { get; set; } = new TextStructure("value");

            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");
        }
        public XlsFindCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public  void Execute(Arguments arguments)
        {
            string res = XlsManager.CurrentXls.Find(arguments.Value.Value);
            if (res != null)
            {
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(res));
            }
            else
            {
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure("-1"));
            }

        }
    }
}
