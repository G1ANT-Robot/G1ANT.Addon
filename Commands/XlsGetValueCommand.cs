﻿using System;
using G1ANT.Language;

namespace G1ANT.Addon.Xls
{
    [Command(Name = "xls.getvalue", Tooltip = "This command allows to get value of specified cell in .xlsx file")]
    public class GetCellValueCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Cell’s row number or row’s name")]
            public IntegerStructure Row { get; set; }

            [Argument(Tooltip = "Cell's column index")]
            public IntegerStructure ColIndex { get; set; }

            [Argument(Tooltip = "Cell's column name")]
            public TextStructure ColName { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public GetCellValueCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            object col = null;
            try
            {
                int row = arguments.Row.Value;
                if (arguments.ColIndex != null)
                    col = arguments.ColIndex.Value;
                else if (arguments.ColName != null)
                    col = arguments.ColName.Value;
                else
                    throw new ArgumentException("One of the ColIndex or ColName arguments have to be set up.");

                var result = new TextStructure(XlsManager.CurrentXls.GetValue(row, col.ToString()));
                Scripter.Variables.SetVariableValue(arguments.Result.Value, result);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while getting value from specified cell. Row: {arguments.Row.Value}. Column: '{col?.ToString()}'. Message: '{ex.Message}'", ex);
            }
            
        }
    }
}
