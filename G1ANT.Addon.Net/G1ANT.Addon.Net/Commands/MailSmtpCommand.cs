/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using System.Text;
using System.Net.Mail;

using G1ANT.Language;
using System.Linq;

namespace G1ANT.Addon.Net
{
    [Command(Name = "mail.smtp", Tooltip = "This command tries to send the file specified by filename.")]
    public class MailSmtpCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Login of user who is sending the email")]
            public TextStructure Login { get; set; }

            [Argument(Required = true, Tooltip = "Password of user who is sending the email")]
            public TextStructure Password { get; set; }

            [Argument(Tooltip = "Smpt client port")]
            public IntegerStructure Port { get; set; } = new IntegerStructure(587);

            [Argument(Tooltip = "Smpt client host name")]
            public TextStructure Host { get; set; } = new TextStructure("smtp.gmail.com");

            [Argument(Required = true, Tooltip = "Sender's email address")]
            public TextStructure From { get; set; }

            [Argument(Required = true, Tooltip = "Receiver's email address")]
            public TextStructure To { get; set; }

            [Argument(Required = true, Tooltip = "Carbon copy")]
            public TextStructure Cc { get; set; } = new TextStructure(string.Empty);

            [Argument(Required = true, Tooltip = "Blind carbon copy")]
            public TextStructure Bcc { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "Mail subject")]
            public TextStructure Subject { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "Mail body, main content of the email ")]
            public TextStructure Body { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "If true body is expexted in HTML format")]
            public BooleanStructure IsHtmlBody { get; set; } = new BooleanStructure(false);

            [Argument(Tooltip = "Array of full paths to all attached files")]
            public ListStructure Attachments { get; set; }

            [Argument(DefaultVariable = "timeoutmailsmtp")]
            public override TimeSpanStructure Timeout { get; set; }
        }
        public MailSmtpCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            SmtpClient client = new SmtpClient();

            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.Port = arguments.Port.Value;
            client.Host = arguments.Host.Value;
            client.Credentials = new System.Net.NetworkCredential(arguments.Login.Value, arguments.Password.Value);
            client.Timeout = (int)arguments.Timeout.Value.TotalMilliseconds;

            var from = arguments.From.Value;
            var to = arguments.To.Value;
            var cc = arguments.Cc.Value.Split(';').ToList(); 
            var bcc = arguments.Bcc.Value.Split(';').ToList();
            var subject = arguments.Subject.Value;
            var body = arguments.Body.Value;

            MailMessage mm = new MailMessage(from, to, subject, body);
            cc.Where(a => !string.IsNullOrEmpty(a.Trim()))
                .ToList()
                .ForEach(a =>mm.CC.Add(a));
            bcc.Where(a => !string.IsNullOrEmpty(a.Trim()))
                .ToList()
                .ForEach(a => mm.Bcc.Add(a));
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.IsBodyHtml = arguments.IsHtmlBody.Value;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            if (arguments.Attachments != null && arguments.Attachments.Value != null)
            {
                foreach (var attachment in arguments.Attachments.Value)
                {
                    mm.Attachments.Add(new Attachment(attachment.ToString()));
                }
            }

            client.Send(mm);
            mm.Attachments.Dispose();
        }
    }
}
