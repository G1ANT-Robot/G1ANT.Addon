using System;
using G1ANT.Language;

namespace G1ANT.Addon.Xls
{
    [Command(Name = "xls.open", Tooltip = "This command allows to open .xlsx files, and set the first sheet in the document as active.")]
    public class XlsOpenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Path { get; set; } = new TextStructure(string.Empty);

            [Argument(Required = false)]
            public TextStructure AccessMode { get; set; } = new TextStructure(string.Empty);
            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");
        }
        public XlsOpenCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            string returVariableName = arguments.Result.Value;
            var xlsWraper = XlsManager.AddXls();
            try
            {
                if (xlsWraper.Open(arguments.Path.Value, arguments.AccessMode.Value))
                {
                    Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.IntegerStructure(xlsWraper.Id));
                    OnScriptEnd = () =>
                    {
                        XlsManager.Remove(xlsWraper);
                    };
                }
                else
                {
                    Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.IntegerStructure(-1));
                }
            }
            catch (Exception ex)
            {
                if (xlsWraper != null)
                {
                    XlsManager.Remove(xlsWraper);
                }
                throw ex;
            }
        }
    }
}
