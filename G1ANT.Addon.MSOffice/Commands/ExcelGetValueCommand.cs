using System;

using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.getvalue", Tooltip = "Gets value from specified cell.", IsUnderConstruction = false)]
    public class ExcelGetValueCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Cell’s row number or row’s name")]
            public IntegerStructure Row { get; set; }

            [Argument(Required = true, Tooltip = "cell’s column number or column’s name")]
            public Structure Col { get; set; } 

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

        }
        public ExcelGetValueCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            object column = null;
            try
            {
                int row = arguments.Row.Value;               
                if (arguments.Col is IntegerStructure)
                    column = (arguments.Col as IntegerStructure).Value;
                else if (arguments.Col is TextStructure)
                    column = (arguments.Col as TextStructure).Value;
                else
                    throw new ArgumentException("Col argument is not valid. It has to be either String or Integer");

                string val = ExcelManager.CurrentExcel.GetCellValue(row, column);
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(val));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while getting value from specified cell. Row: {arguments.Row.Value}. Column: '{column?.ToString()}'. Message: '{ex.Message}'", ex);
            }
        }
    }
}
