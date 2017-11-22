
using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "outlook.findmails", Tooltip = "This command allows to search mails in Inbox and returns all mails that contain provided word in the subject.", NeedsDelay = true, IsUnderConstruction = false)]
    public class OutlookFindMailsCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true,Tooltip = "Word to be searched in subject")]
            public TextStructure Search { get; set; }

            [Argument]
            public BooleanStructure ShowMail { get; set; } = new BooleanStructure(false);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument]
            public BooleanStructure If { get; set; } = new BooleanStructure(true);

            [Argument]
            public TextStructure ErrorJump { get; set; }

            [Argument]
            public TextStructure ErrorMessage { get; set; }
        }
        public OutlookFindMailsCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            var outlookManager = OutlookManager.CurrentOutlook;
            var search = arguments.Search.Value;
            var showMails = arguments.ShowMail.Value;
            if (search!="")
            {
                outlookManager.FindMails(search, showMails);
            }
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(outlookManager.IsMailFound.ToString()));
        }
    }
}
