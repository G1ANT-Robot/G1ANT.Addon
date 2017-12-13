
using G1ANT.Language;
using System;
using System.Linq;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.getrow", Tooltip = "Gets all used cells of the specified row.")]
    public class ExcelGetRowCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Cell's row number or row's name")]
            public IntegerStructure Row { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public ExcelGetRowCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                var val = ExcelManager.CurrentExcel.GetRow(arguments.Row.Value); 
                var structureDictionary = val.ToDictionary(x => x.Key, x => (Structure)new TextStructure(x.Value));
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(structureDictionary)); 
            } 
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while getting row: '{arguments.Row.Value}'. Message: {ex.Message}", ex);
            }
        }
    }
}
