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
    [Command(Name = "outlook.send",Tooltip = "This command to send a prepared earlier message by 'outlook.newmessage'.", NeedsDelay = true, IsUnderConstruction = false)]
    public class OutlookSendCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");


        }
        public OutlookSendCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            var outlookManager = OutlookManager.CurrentOutlook; ;
            outlookManager.Send();
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.BooleanStructure(true));
        }
    }
}
