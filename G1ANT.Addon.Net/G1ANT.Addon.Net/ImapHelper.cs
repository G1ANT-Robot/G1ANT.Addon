using MailKit;
using MailKit.Net.Imap;
using System;
using System.Net;

namespace G1ANT.Addon.Net
{
    public static class ImapHelper
    {
        private static void ConnectClient(ImapClient client, NetworkCredential credentials, Uri uri, bool readOnly)
        {
            client.Connect(uri);
            client.Authenticate(credentials);
            client.Inbox.Open(readOnly ? FolderAccess.ReadOnly : FolderAccess.ReadWrite);
            client.Inbox.Subscribe();
        }

        public static ImapClient CreateImapClient(NetworkCredential credentials, Uri uri, bool readOnly, int timeout)
        {
            var client = new ImapClient {Timeout = timeout};
            ConnectClient(client, credentials, uri, readOnly);
            return client;
        }
    }
}
