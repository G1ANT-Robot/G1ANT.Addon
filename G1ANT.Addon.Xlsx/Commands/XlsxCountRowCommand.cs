using G1ANT.Language;

namespace G1ANT.Addon.Xlsx
{
    [Command(Name = "xls.countrows",Tooltip = "This command allows to count rows in open .xlsx file.")]
    public class XlsxCountRowsCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public XlsxCountRowsCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public  void Execute(Arguments arguments)
        {
            try
            {
                int res = XlsxManager.CurrentXls.CountRows();
                
                   Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.IntegerStructure(res));
            }
            catch
            {
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.IntegerStructure(-1));
            }
        }

      
    }
}
