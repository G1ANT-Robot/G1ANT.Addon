/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.MSOffice
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
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

            [Argument(Tooltip = "Cell's column index")]
            public IntegerStructure ColIndex { get; set; }

            [Argument(Tooltip = "Cell's column name")]
            public TextStructure ColName { get; set; }
        }
        public ExcelSetValueCommand(AbstractScripter scripter) : base(scripter)
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
                ExcelManager.CurrentExcel.SetCellValue(arguments.Row.Value, col, arguments.Value.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while setting value. Row: '{arguments.Row.Value}', Col: '{col}', Val: '{arguments.Value.Value}'", ex);
            }
        }
    }
}
