using System;

using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.insertcolumn", Tooltip = "Inserts empty column.")]
    public class ExcelInsertColumnCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Column's address")]
            public Structure Column { get; set; }

            [Argument(Tooltip = "Determines, whether to insert column 'before' or 'after' specified column. By default: 'after'")]
            public TextStructure Where { get; set; } = new TextStructure("after");
        }
        public ExcelInsertColumnCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            object col = null;
            try
            {                
                if (arguments.Column is IntegerStructure)
                {
                    col = (arguments.Column as IntegerStructure).Value;
                }
                else if (arguments.Column is TextStructure)
                {
                    col = (arguments.Column as TextStructure).Value;
                }
                else
                {
                    throw new ArgumentException("Col argument is not valid. It has to be either String or Integer.");
                }
                ExcelManager.CurrentExcel.InsertColumn(col, arguments.Where.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while trying to insert column. Column: '{col}', where: '{arguments.Where.Value}'. Message: {ex.Message}", ex);
            }            
        }
    }
}
