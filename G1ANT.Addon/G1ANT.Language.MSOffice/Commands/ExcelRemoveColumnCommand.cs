using System;

using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.removecolumn", Tooltip = "Removes column.")]
    public class ExcelRemoveColumnCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Column's address")]
            public Structure Column { get; set; }
        }
        public ExcelRemoveColumnCommand(AbstractScripter scripter) : base(scripter)
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
                ExcelManager.CurrentExcel.RemoveColumn(col);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while trying to remove column. Column: '{col}'. Message: {ex.Message}", ex);
            }            
        }
    }
}
