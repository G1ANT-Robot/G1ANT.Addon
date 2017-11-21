﻿
using G1ANT.Language;


using System;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.export", Tooltip = "Exports currently active excel workbook to either *.pdf or *.xps file.")]
    public class ExcelExportCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Path { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public BooleanStructure If { get; set; } = new BooleanStructure(true);

            [Argument]
            public TextStructure ErrorJump { get; set; }

            [Argument]
            public TextStructure ErrorMessage { get; set; }
        }

        public ExcelExportCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            try
            {
                ExcelManager.CurrentExcel.Export(arguments.Path.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while exporting currently opened workbook. Path: {arguments.Path.Value}. Message: '{ex.Message}'", ex);
            }
        }
    }
}
