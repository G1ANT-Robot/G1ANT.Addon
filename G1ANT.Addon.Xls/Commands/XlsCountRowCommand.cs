using G1ANT.Language;

namespace G1ANT.Addon.Xls
{
    [Command(Name = "xls.countrows",Tooltip = "This command allows to count rows in open .xlsx file.")]
    public class XlsCountRowsCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public XlsCountRowsCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public  void Execute(Arguments arguments)
        {
            try
            {
                int res = XlsManager.CurrentXls.CountRows();
                
                   Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.IntegerStructure(res));
            }
            catch
            {
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.IntegerStructure(-1));
            }
        }

      
    }
}
