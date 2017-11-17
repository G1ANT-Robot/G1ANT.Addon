using System;

using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.insertrow", Tooltip = "Inserts empty row.")]
    public class ExcelInsertRowCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Row's number")]
            public IntegerStructure Row { get; set; }

            [Argument(Tooltip = "Determines, whether to insert row 'below' or 'above' specified row. By default: 'below'")]
            public TextStructure Where { get; set; } = new TextStructure("below");
        }
        public ExcelInsertRowCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                ExcelManager.CurrentExcel.InsertRow(arguments.Row.Value, arguments.Where.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while trying to insert row. Row: '{arguments.Row.Value}', where: '{arguments.Where.Value}'. Message: {ex.Message}", ex);
            }            
        }
    }
}
