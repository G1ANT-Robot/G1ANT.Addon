
using G1ANT.Language;


using System;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.getformula", Tooltip = "Gets formula from specified cell.")]
    public class ExcelGetFormulaCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Cell's row number or row's name")]
            public IntegerStructure Row { get; set; }

            [Argument(Required = true, Tooltip = "Cell's column number or column's name")]
            public Structure Col { get; set; } //TODO Rozbic na dwa argumenty index i name
            
            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");
        }
        public ExcelGetFormulaCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            object col = null;
            try
            {                
                if (arguments.Col is IntegerStructure)
                    col = (arguments.Col as IntegerStructure).Value;
                else if (arguments.Col is TextStructure)
                    col = (arguments.Col as TextStructure).Value;
                else
                    throw new ArgumentException("Col argument is not valid. It has to to be either String or Integer value.");
                string formula = ExcelManager.CurrentExcel.GetFormula(arguments.Row.Value, col);
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(formula));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while getting formula. Col: '{col}', Row: '{arguments.Row.Value}'. Message: {ex.Message}", ex);
            }
        }
    }
}
