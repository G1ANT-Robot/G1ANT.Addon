
using G1ANT.Language;


using System;
using System.IO;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.open", Tooltip= "Opens new excel instance.")]
    public class ExcelOpenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public TextStructure Path { get; set; }

            [Argument]
            public BooleanStructure InBackground { get; set; } = new BooleanStructure(false);

            [Argument]
            public TextStructure Sheet { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public ExcelOpenCommand(AbstractScripter scripter) : base(scripter)
        {
        }


        public void Execute(Arguments arguments)
        {
            try
            {
                ExcelWrapper excelWrapper = ExcelManager.CreateInstance();
                excelWrapper.Open(arguments.Path?.Value, arguments.Sheet?.Value, !arguments.InBackground.Value);
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.IntegerStructure(excelWrapper.Id));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while opening excel instance. Path: '{arguments.Path.Value}', Sheet: '{arguments.Sheet?.Value}', InBackground: '{arguments.InBackground.Value}'", ex);
            }           
        }
    }
}
