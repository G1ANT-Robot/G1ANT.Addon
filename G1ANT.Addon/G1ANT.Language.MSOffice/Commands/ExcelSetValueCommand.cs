using System;

using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.setvalue", Tooltip = "Sets value in specified cell.")]
    public class ExcelSetValueCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Value { get; set; }

            [Argument(Required = true, Tooltip = "Cell's row number")]
            public IntegerStructure Row { get; set; }

            [Argument(Required = true, Tooltip = "cell's column number or name")]
            public Structure Col { get; set; }

            [Argument]
            public BooleanStructure If { get; set; } = new BooleanStructure(true);

            [Argument]
            public TextStructure ErrorJump { get; set; }

            [Argument]
            public TextStructure ErrorMessage { get; set; }
        }
        public ExcelSetValueCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            object col = null;
            try
            {                
                if (arguments.Col is IntegerStructure)
                {
                    col = (arguments.Col as IntegerStructure).Value;
                }
                else if (arguments.Col is TextStructure)
                {
                    col = (arguments.Col as TextStructure).Value;
                }
                else
                {
                    throw new ArgumentException("Col argument is not valid. It has to be either String or Integer.");
                }
                ExcelManager.CurrentExcel.SetCellValue(arguments.Row.Value, col, arguments.Value.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while setting value. Row: '{arguments.Row.Value}', Col: '{col}', Val: '{arguments.Value.Value}'", ex);
            }
        }
    }
}
