
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
            public TextStructure Result { get; set; } = new TextStructure("result");
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
