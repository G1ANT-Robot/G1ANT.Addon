using System;
using System.Linq;
using System.Text.RegularExpressions;
using G1ANT.Language;
using WatiN.Core;
using WatiN.Core.Constraints;
using System.Threading.Tasks;

namespace G1ANT.Addon.Mscrm
{
    [Command(Name = "mscrm.getvalue", Tooltip = "This command allows getting field value based on field id.")]
    public class MsCrmGetValueCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public TextStructure Search { get; set; }

            [Argument(DefaultVariable = "timeoutcrm")]
            public override TimeSpanStructure Timeout { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public MsCrmGetValueCommand(AbstractScripter scripter) : base(scripter)
        { }

        // this method should be implemented even though its not yet required by Command abstract class
        public void Execute(Arguments arguments)
        {
            try
            {
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure("null"));
                if (MsCrmManager.CurrentCRM != null)
                {
                    Document frame = MsCrmManager.CurrentCRM.GetCurrentIframe();
                    if (frame != null)
                    {
                        Div e = frame.Div(arguments.Search.Value);
                        if (e != null)
                        {
                            string value = "";
                            Div div = frame.Div(e.Id)?.Div(Find.Any);
                            Label label = div?.Label(Find.Any);
                            Span span1 = (Span)label.NextSibling;
                            Span span2 = (Span)span1.Span(Find.Any);
                            if (span2 != null && span2.Exists)
                            {
                                string content = span2.InnerHtml;
                                if (span2.ClassName == "ms-crm-Lookup-Item")
                                {
                                    content = span2.OuterText;
                                }
                                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(content));
                            }
                            else
                            {

                                string[] content = label.OuterText.Split('\n');
                                if (content.Length > 1)
                                {
                                    value = content[1];
                                }
                                else
                                {
                                    value = label.OuterText;
                                }

                                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(value));
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //TODO AddLog(ex.Message);
                Scripter.Log.Log(AbstractLogger.Level.Error, ex.Message);
                throw new ApplicationException("Unable to getvalue CRM");
            }
        }

    }
}
