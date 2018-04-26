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
    [Command(Name = "outlook.open", Tooltip = "This command allows to open the outlook program. It must be always executed before other outlook command will be used.", NeedsDelay = true, IsUnderConstruction = false)]
    public class OutlookOpenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public BooleanStructure Display { get; set; } = new BooleanStructure(true);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public OutlookOpenCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            var outlookManager = OutlookManager.AddOutlook();
            outlookManager.Open(arguments.Display.Value);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.BooleanStructure(true));
        }
    }
}
