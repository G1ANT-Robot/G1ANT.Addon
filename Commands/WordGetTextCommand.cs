using G1ANT.Language;
using System;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "word.gettext", Tooltip = "This command gets all text from current document.", NeedsDelay = true, IsUnderConstruction = false)]

    public class WordGetTextCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
            [Argument]
            public BooleanStructure If { get; set; } = new BooleanStructure(true);

            [Argument]
            public TextStructure ErrorJump { get; set; }

            [Argument]
            public TextStructure ErrorMessage { get; set; }

        }
        public WordGetTextCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            WordWrapper wordWrapper = WordManager.CurrentWord;

            try
            {
                string val = wordWrapper.GetText();
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(val));
            }
            catch (Exception ex)
            {

                throw new ApplicationException($"Error occured while trying get text. Message: {ex.Message}", ex);

            }
        }
    }
}
