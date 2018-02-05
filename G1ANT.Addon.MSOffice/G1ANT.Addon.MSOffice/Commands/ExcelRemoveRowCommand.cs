using System;

using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.removerow", Tooltip = "Removes row.")]
    public class ExcelRemoveRowCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Row's number")]
            public IntegerStructure Row { get; set; }
        }
        public ExcelRemoveRowCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                ExcelManager.CurrentExcel.RemoveRow(arguments.Row.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while trying to remove row. Row: '{arguments.Row.Value}'. Message: {ex.Message}", ex);
            }            
        }
    }
}
