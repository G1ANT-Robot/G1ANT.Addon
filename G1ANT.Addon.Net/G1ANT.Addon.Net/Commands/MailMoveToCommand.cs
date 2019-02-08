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
using MailKit;
using MailKit.Net.Imap;
using G1ANT.Language;
using System.Net;


namespace G1ANT.Addon.Net
{
    [Command(Name = "mail.moveto", Tooltip = "This command moves selected message to another folder.")]
    public class MailMoveToCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Host name")]
            public TextStructure Host { get; set; }

            [Argument(Required = true, Tooltip = "Port")]
            public IntegerStructure Port { get; set; } = new IntegerStructure(993);

            [Argument(Required = true, Tooltip = "Login of the inbox user")]
            public TextStructure Login { get; set; }

            [Argument(Required = true, Tooltip = "Password of the inbox user")]
            public TextStructure Password { get; set; }

            [Argument(Required = true, Tooltip = "Mail to move")]
            public MailStructure MailToMove { get; set; }

            [Argument(Required = false, Tooltip = "Name of the origin folder")]
            public TextStructure Origin { get; set; } = new TextStructure("");

            [Argument(Required = false, Tooltip = "Name of the destination folder")]
            public TextStructure Destination { get; set; } = new TextStructure("");

            [Argument(Required = false, Tooltip = "Ignore certificate errors")]
            public BooleanStructure IgnoreCertificateErrors { get; set; } = new BooleanStructure(false);
        }

        public MailMoveToCommand(AbstractScripter scripter) : base(scripter)
        { }


        public void Execute(Arguments arguments)
        {
            if (arguments.IgnoreCertificateErrors.Value)
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            }
            var credentials = new NetworkCredential(arguments.Login.Value, arguments.Password.Value);
            var uri = new UriBuilder("imaps", arguments.Host.Value, arguments.Port.Value).Uri;
            var timeout = (int)arguments.Timeout.Value.TotalMilliseconds;

            var client = ImapHelper.CreateImapClient(credentials, uri, false, timeout);

            if (client.IsConnected && client.IsAuthenticated)
            {
                var destinationFolder = client.GetFolder(arguments.Destination.Value);
                var originFolder = client.GetFolder(arguments.Origin.Value);
                if (destinationFolder != null && originFolder != null)
                {
                    destinationFolder.Open(FolderAccess.ReadWrite);
                    originFolder.Open(FolderAccess.ReadWrite);
                    originFolder.MoveTo(arguments.MailToMove.Value.UniqueId,destinationFolder);
                }
                else
                {
                    throw new NullReferenceException("Folder with specified name does not exist.");
                }
            }
            else
            {
                throw new NullReferenceException("Could not connect to imap server.");
            }
        }
    }
}