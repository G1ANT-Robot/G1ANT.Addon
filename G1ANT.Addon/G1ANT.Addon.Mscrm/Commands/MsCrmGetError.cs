using G1ANT.Language;
using System;

namespace G1ANT.Language.Mscrm
{
    [Command(Name = "mscrm.geterror",Tooltip = "This command allows to capture error message displayed in status bar at the bottom of the form.")]
    public class MsCrmGetError: Command
    {
        public class Arguments : CommandArguments
        {
           [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(DefaultVariable = "timeoutcrm")]
            public override TimeSpanStructure Timeout { get; set; }
        }
        public MsCrmGetError(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(string.Empty));
            try
            {
                var frame = MsCrmManager.CurrentCRM.GetCurrentIframe();
                if (frame != null)
                {
                    var span = frame.Element("titlefooter_statuscontrol");
                    if (span != null)
                    {
                        Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(span.InnerHtml?.Trim() ?? string.Empty));
                    }
                }
            }
            catch
            {
                throw new ApplicationException("Unable to geterror from CRM");
            }
        }
    }
}
