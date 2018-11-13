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

using MailKit;
using MailKit.Net.Imap;

using G1ANT.Language;

namespace G1ANT.Addon.Net
{
    using System.Net;

    using Language.Structures;

    using MimeKit;


    [Command(Name = "mail.imap", Tooltip = "This command tries to retrieve the mail specified by filename.")]
    public class MailImapCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Smpt client host name")]
            public TextStructure Host { get; set; }

            [Argument(Tooltip = "Smpt client port")]
            public IntegerStructure Port { get; set; } = new IntegerStructure();

            [Argument(Required = true, Tooltip = "Login of the inbox user")]
            public TextStructure Login { get; set; }

            [Argument(Required = true, Tooltip = "Password of the inbox user")]
            public TextStructure Password { get; set; }

            [Argument(Required = true, Tooltip = "Since what date should emails be retrieved")]
            public DateStructure SinceDate { get; set; }

            [Argument(Required = true, Tooltip = "To what date should emails be retrieved")]
            public DateStructure ToDate { get; set; }

            [Argument(Required = true, Tooltip = "Look only for already unread messages")]
            public BooleanStructure OnlyUnreadMessages { get; set; }

            [Argument(Required = false, Tooltip = "Mark analyzed messages as read")]
            public BooleanStructure MarkAsRead { get; set; }

            [Argument(Required = false, Tooltip = "Received messages")]
            public VariableStructure Result { get; set; } = new VariableStructure("ReceivedMessages");
        }


        public MailImapCommand(AbstractScripter scripter) : base(scripter)
        { }


        public void Execute(Arguments arguments)
        {
            var client = new ImapClient();
            var credentials = new NetworkCredential(arguments.Login.Value, arguments.Password.Value);
            var uri = new UriBuilder("imaps", arguments.Host.Value, arguments.Port.Value).Uri;
            var timeout = (int)arguments.Timeout.Value.TotalMilliseconds;
            var markAsRead = arguments.MarkAsRead.Value;

            ClientConnection(client, credentials, uri, timeout, markAsRead == false);

            if (client.IsConnected && client.IsAuthenticated)
            {
                var messages = ReceiveMesssages(client, arguments);

                SendMessageListToScripter(client, arguments, messages);

                if (markAsRead)
                {
                    MarkMessagesAsRead(client, messages);
                }
            }
        }

        private void ClientConnection(ImapClient client, NetworkCredential credentials, Uri uri, int timeout, bool readOnly)
        {
            client.Timeout = timeout;
            client.ServerCertificateValidationCallback = (s, c, h, e) => true; //HACK try to Repair & Remove
            client.Connect(uri);
            client.Authenticate(credentials);
            client.Inbox.Open(readOnly ? FolderAccess.ReadOnly : FolderAccess.ReadWrite);
            client.Inbox.Subscribe();
        }

        private void SendMessageListToScripter(ImapClient client, Arguments arguments, List<IMessageSummary> messages)
        {
            var messageList = new ListStructure();

            foreach (var message in messages)
            {
                var messageWithFolder = new SimplifiedMessageSummary(message as MessageSummary, client.Inbox);
                var structure = new MailStructure(messageWithFolder, null, null);
                messageList.AddItem(structure);
            }

            Scripter.Variables.SetVariableValue(arguments.Result.Value, messageList);
        }


        private List<IMessageSummary> ReceiveMesssages(ImapClient client, Arguments arguments)
        {
            var allMessages = client.Inbox.Fetch(0, -1, MessageSummaryItems.All | MessageSummaryItems.Body | MessageSummaryItems.BodyStructure | MessageSummaryItems.UniqueId).ToList();
            var onlyUnread = arguments.OnlyUnreadMessages.Value;
            var since = arguments.SinceDate.Value;
            var to = arguments.ToDate.Value;
            return SelectMessages(allMessages, onlyUnread, since, to);
        }

        private void MarkMessagesAsRead(ImapClient client, List<IMessageSummary> messages)
        {
            foreach (IMessageSummary message in messages)
            {
                client.Inbox.SetFlags(message.UniqueId, MessageFlags.Seen, true);
            }
        }

        private List<IMessageSummary> SelectMessages(List<IMessageSummary> messages, bool onlyUnRead, DateTime sinceDate,
                                                     DateTime toDate)
        {
            Func<IMessageSummary, bool> isUnread = m => m.Flags != null && m.Flags.Value.HasFlag(MessageFlags.Seen) == false;
            var relevantMessages = onlyUnRead ? messages.Where(isUnread).ToList() : messages;

            relevantMessages = relevantMessages.Where(m => m.Date >= sinceDate && m.Date <= toDate).ToList();
            return relevantMessages;
        }
    }
}
