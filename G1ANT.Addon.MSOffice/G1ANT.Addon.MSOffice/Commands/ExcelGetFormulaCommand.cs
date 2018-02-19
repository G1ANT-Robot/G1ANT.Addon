﻿/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.MSOffice
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

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

            [Argument(Tooltip = "Cell's column index")]
            public IntegerStructure ColIndex { get; set; }

            [Argument(Tooltip = "Cell's column name")]
            public TextStructure ColName { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public ExcelGetFormulaCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            object col = null;
            try
            {
                if (arguments.ColIndex != null)
                    col = arguments.ColIndex.Value;
                else if (arguments.ColName != null)
                    col = arguments.ColName.Value;
                else
                    throw new ArgumentException("One of the ColIndex or ColName arguments have to be set up.");
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
