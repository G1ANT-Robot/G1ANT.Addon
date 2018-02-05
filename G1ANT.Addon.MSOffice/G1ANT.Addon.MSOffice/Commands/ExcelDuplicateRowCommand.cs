﻿using System;

using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.duplicaterow", Tooltip = "Duplicates row.")]
    public class ExcelDuplicateRowCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Source row's number")]
            public IntegerStructure Source { get; set; }

            [Argument(Required = true, Tooltip = "Destination row's number")]
            public IntegerStructure Destination { get; set; }
        }
        public ExcelDuplicateRowCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                ExcelManager.CurrentExcel.DuplicateRow(arguments.Source.Value, arguments.Destination.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while trying to duplicate row. Source: '{arguments.Source.Value}', destination: '{arguments.Destination.Value}'. Message: {ex.Message}", ex);
            }            
        }
    }
}
