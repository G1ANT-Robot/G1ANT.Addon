/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.MSOffice
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "word.open",Tooltip = "This command opens word program.", NeedsDelay = true, IsUnderConstruction = false)]
    public class WordOpenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Path { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");


        }

        public WordOpenCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            string path = arguments.Path.Value;

            WordWrapper wordWraper = WordManager.AddWord();
            wordWraper.Open(path);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.IntegerStructure(wordWraper.Id));
        }
    }
}
