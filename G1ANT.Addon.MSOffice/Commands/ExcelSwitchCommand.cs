
using G1ANT.Language;


using System.Linq;
using System;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.switch", Tooltip = "This command allows to switch from one excel instance to another")]
    public class ExcelSwitchCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Id number or variable name of excel instance that will be activated")]
            public IntegerStructure Id { get; set; }

        }

        public ExcelSwitchCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                ExcelManager.SwitchExcel(arguments.Id.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while switching to another excel instance. Id: '{arguments.Id.Value}'. Message: '{ex.Message}'");
            }
        }
    }
}