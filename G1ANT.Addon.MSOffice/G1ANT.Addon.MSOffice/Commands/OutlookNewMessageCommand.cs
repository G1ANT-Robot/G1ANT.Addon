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


using System.Collections.Generic;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "outlook.newmessage",Tooltip="This command allows to open a new message window and fills it up with provided arguments.", NeedsDelay = true, IsUnderConstruction = false)]
    public class OutlookNewMessageCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public TextStructure To { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public TextStructure Subject { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public TextStructure Body { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public ListStructure Attachments { get; set; } = new ListStructure(new List<Structure>() { null });

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public OutlookNewMessageCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            var outlookManager = OutlookManager.CurrentOutlook;
            var to = arguments.To.Value;
            var subject = arguments.Subject.Value;
            var body = arguments.Body.Value;
            bool nullAttachement = false;

            foreach (var arg in arguments.Attachments.Value)
            {
                if (arg == null)
                {
                    nullAttachement = true;
                }
            }

            if (nullAttachement)
            {
                outlookManager.NewMessage(to, subject, body);
            }
            else
            {
                List<object> pathsObject = arguments.Attachments.Value;
                List<string> paths = new List<string>();
                for (int i = 0; i < pathsObject.Count; i++)
                {
                    string p = pathsObject[i].ToString().ToUpper();
                    paths.Add(p);
                }
                outlookManager.NewMessageWithAttachements(to, subject, body, paths);

            }
            //SetVariableValue(arguments.Result.Value, new Language.BooleanStructure(true));
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure("true"));
        }
    }
}
