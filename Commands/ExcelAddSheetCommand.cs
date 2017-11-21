using G1ANT.Language;
using System;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.addsheet", Tooltip = "Adds new sheet.")]
    public class ExcelAddSheetCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Name of sheet to be added")]
            public TextStructure Name { get; set; } = new TextStructure(string.Empty);
        }
        public ExcelAddSheetCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            try
            {
                ExcelManager.CurrentExcel.AddSheet(arguments.Name.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while adding sheet to current excel instance. Name: '{arguments.Name.Value}'. Message: '{ex.Message}'", ex);
            }
        }
    }
}
