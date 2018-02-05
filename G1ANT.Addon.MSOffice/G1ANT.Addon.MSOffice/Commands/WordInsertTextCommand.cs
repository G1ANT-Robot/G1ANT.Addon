﻿
using G1ANT.Language;


using System;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "word.inserttext", Tooltip = "This command inserts text into current document.", NeedsDelay = true, IsUnderConstruction = false)]

    public class WordInsertTextCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "text to be placed into document")]
            public TextStructure Text { get; set; } = new TextStructure(string.Empty);
            [Argument]
            public BooleanStructure ReplaceAllText { get; set; } = new BooleanStructure(false);

        }
        public WordInsertTextCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            WordWrapper wordWrapper = WordManager.CurrentWord;
            string text = arguments.Text.Value;
            bool replaceAlltext = arguments.ReplaceAllText.Value;

            try
            {
                wordWrapper.InsertText(text, replaceAlltext);
            }
            catch (Exception ex)
            {

                throw new ApplicationException($"Error occured while trying to insert text. Text: '{arguments.Text.Value}'. Message: {ex.Message}", ex);

            }
        }
    }
}
