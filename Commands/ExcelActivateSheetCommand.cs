using System;

using G1ANT.Language;
using System.Windows.Forms;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.activatesheet", Tooltip = "Activates the specified sheet in currently active excel instance.")]
    public class ExcelActivateSheetCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Name of sheet to be activated")]
            public TextStructure Name { get; set; }
        }
        public ExcelActivateSheetCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            try
            {
                ExcelManager.CurrentExcel.ActivateSheet(arguments.Name.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while activating sheet. Sheet name: '{arguments.Name.Value}' Message: '{ex.Message}'", ex);
            }
        }
    }
}
