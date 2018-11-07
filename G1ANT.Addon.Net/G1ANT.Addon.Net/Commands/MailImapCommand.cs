/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Net.Mail;

using MailKit;
using MailKit.Net.Imap;
using G1ANT.Language;

namespace G1ANT.Addon.Net
{    
    using System.Net;

    using Language.Structures;


    [Command(Name = "mail.imap", Tooltip = "This command tries to retrieve the mail specified by filename.")]
    public class MailImapCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Smpt client host name")]
            public TextStructure Host { get; set; }

            [Argument(Tooltip = "Smpt client port")]
            public IntegerStructure Port { get; set; } = new IntegerStructure();

            [Argument(Required = true, Tooltip = "Login of user who is sending the email")]
            public TextStructure Login { get; set; }

            [Argument(Required = true, Tooltip = "Password of user who is sending the email")]
            public TextStructure Password { get; set; }

            [Argument(Required = true, Tooltip = "Since what date should emails be retrieved")]
            public DateStructure SinceDate { get; set; }

            [Argument(Required = true, Tooltip = "Password of user who is sending the email")]
            public DateStructure ToDate { get; set; }

            [Argument(Required = true, Tooltip = "Look only for already read messages")]
            //public BooleanStructure OnlyReadMessages { get; set; }
            public BooleanStructure OnlyUnreadMessages { get; set; }

            //[Argument(Required = true, Tooltip = "Since what date should emails be retrieved")]
            //public ListStructure MessageList { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

        }


        public MailImapCommand(AbstractScripter scripter) : base(scripter)
        { }


        public void Execute(Arguments arguments)
        {
            var client = new ImapClient();
            var credentials = new NetworkCredential(arguments.Login.Value, arguments.Password.Value);
            var uri = new UriBuilder("imaps", arguments.Host.Value, arguments.Port.Value).Uri;
            ClientConnection(client, credentials, uri, (int)arguments.Timeout.Value.TotalMilliseconds);

            if (client.IsConnected && client.IsAuthenticated)
            {
                ReceiveMesssages(client, arguments);
            }
        }

        private void ClientConnection(ImapClient client, NetworkCredential credentials, Uri uri, int timeout)
        {
            client.Timeout = timeout;
            client.ServerCertificateValidationCallback = (s, c, h, e) => true; //HACK try to Repair & Remove
            client.Connect(uri);
            client.Authenticate(credentials);
            client.Inbox.Open(FolderAccess.ReadOnly);
            client.Inbox.Subscribe();
        }

        //TODO: Use some kind of enumerator structure instead of boolean structure
        private List<IMessageSummary> SelectRelevantMessages(List<IMessageSummary> messages, BooleanStructure onlyRead, DateTime sinceDate)
        {
            List<IMessageSummary> relevant = new List<IMessageSummary>();
            if (onlyRead.Value)
            {
                relevant = messages.Where(m => m.Flags != null && m.Flags.Value.HasFlag(MessageFlags.Seen) != onlyRead.Value)
                                   .ToList();
            }
            else
            {
                relevant = messages;
            }
            return relevant;
            //return relevantMessages.Where(m => m.Date.DateTime > sinceDate).ToList();
        }


        //TODO: Should be further developed after creating Mail Structure
        private void ReceiveMesssages(ImapClient client, Arguments arguments)
        {
            var messages = client.Inbox.Fetch(0, -1, MessageSummaryItems.Full | MessageSummaryItems.UniqueId).ToList();
            var selectedMessages = SelectRelevantMessages(messages, arguments.OnlyUnreadMessages, arguments.SinceDate.Value);
            int i = 0;

            ListStructure messageList = new ListStructure();;
            foreach (IMessageSummary message in selectedMessages)
            {
                var structure = new MailStructure(message);
                messageList.AddItem(structure);
            }
            Scripter.Variables.SetVariableValue(arguments.Result.Value, messageList);

            //MailStructure structure;
            //Scripter.Variables.SetVariableValue("messages", List<stru>);
        }
    }
}
