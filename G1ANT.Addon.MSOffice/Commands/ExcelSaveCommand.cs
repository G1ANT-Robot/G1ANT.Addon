
using G1ANT.Language;


using System;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.save", Tooltip = "Saves currently active excel workbook.")]
    public class ExcelSaveCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Saving file path. If not specified, G1ANT robot will try to save the file under the path it was loaded from. \nIf current excel application was not opened with specified path, exception will be thrown.")]
            public TextStructure Path { get; set; }

            [Argument]
            public BooleanStructure If { get; set; } = new BooleanStructure(true);

            [Argument]
            public TextStructure ErrorJump { get; set; }

            [Argument]
            public TextStructure ErrorMessage { get; set; }

        }
        public ExcelSaveCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                ExcelManager.CurrentExcel.Save(arguments.Path?.Value);

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while saving currently opened excel workbook. Path: '{arguments.Path.Value}'", ex);
            }
        }
    }
}
