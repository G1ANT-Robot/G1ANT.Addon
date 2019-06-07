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
    [Command(Name = "mail.smtp", Tooltip = "This command sends a mail message from a provided email address to a specified recipient")]
    public class MailSmtpCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "User email login")]
            public TextStructure Login { get; set; }

            [Argument(Required = true, Tooltip = "User email password")]
            public TextStructure Password { get; set; }

            [Argument(Tooltip = "SMTP server port number")]
            public IntegerStructure Port { get; set; } = new IntegerStructure(587);

            [Argument(Tooltip = "SMTP server address")]
            public TextStructure Host { get; set; } = new TextStructure("smtp.gmail.com");

            [Argument(Required = true, Tooltip = "Sender's email address")]
            public TextStructure From { get; set; }

            [Argument(Required = true, Tooltip = "Recipient's email address")]
            public TextStructure To { get; set; }

            [Argument(Tooltip = "Carbon copy address(es); use semicolon (;) to separate multiple addresses")]
            public TextStructure Cc { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "Blind carbon copy address(es); use semicolon (;) to separate multiple addresses")]
            public TextStructure Bcc { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "Message subject")]
            public TextStructure Subject { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "Message body, i.e. the main content of an email")]
            public TextStructure Body { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "If true body is expexted in HTML format")]
            public BooleanStructure IsHtmlBody { get; set; } = new BooleanStructure(false);

            [Argument(Tooltip = "List of full paths to all files to be attached")]
            public ListStructure Attachments { get; set; }

            [Argument(DefaultVariable = "timeoutmailsmtp", Tooltip = "Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed")]
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
