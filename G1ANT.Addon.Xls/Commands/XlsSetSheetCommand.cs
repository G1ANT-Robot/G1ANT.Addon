using System;
using G1ANT.Language;

namespace G1ANT.Language.Xls
{
    [Command(Name = "xls.setsheet", Tooltip = "This command allows to set active sheet to work with.")]
    public class XlsSetSheetCommand : Command
    {
        public  class Arguments : CommandArguments
        {
            [Argument]
            public TextStructure Name { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");
        }
        public XlsSetSheetCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public  void Execute(Arguments arguments)
        {
            if (arguments.Name.Value == string.Empty)
            {
                try
                {
                    XlsManager.CurrentXls.ActivateSheet(XlsManager.CurrentXls.GetSheetsNames()[0]);
                    Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(true));
                }
                catch
                {
                    Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(false));
                    throw new InvalidOperationException("Workbook do not have any sheet");
                }
            }
            else
            {
                try
                {
                    XlsManager.CurrentXls.ActivateSheet(arguments.Name.Value);
                    Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(true));
                }
                catch
                {
                    Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(false));
                    throw new ArgumentOutOfRangeException(nameof(arguments.Name), arguments.Name.Value, $"Workbook do not have '{arguments.Name.Value} sheet");
                }
            }
        }
    }
}
