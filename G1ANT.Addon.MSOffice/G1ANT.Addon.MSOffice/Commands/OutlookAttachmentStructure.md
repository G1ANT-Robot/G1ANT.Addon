# outlookattachment

This structure stores information about the attachment to a mail message, which was retrieved with the `outlook.getfolder` command. It contains two fields:

| Field      | Type                                                         | Description                         |
| ---------- | ------------------------------------------------------------ | ----------------------------------- |
| `filename` | [text](../../../g1ant.language/structures/textstructure.md)  | The filename of the attachment      |
| `size`     | [integer](../../../g1ant.language/structures/integerstructure.md) | The size of the attachment in bytes |

## Example

The following script reads emails from the Outlook Inbox folder, then processes them one by one and displays information about any attachments, using the `♥attachment` variable, which is of the `outlookattachment` structure (be sure to provide the correct Outlook folder information in the `♥outlookInboxFolder` variable):

```G1ANT
♥outlookInboxFolder = john.doe@g1ant.com\Inbox

outlook.open display false
outlook.getfolder ♥outlookInboxFolder result ♥InboxFolder errormessage ‴Cannot find folder "♥outlookInboxFolder"‴
♥emails = ♥InboxFolder⟦mails⟧
foreach ♥email in ♥emails
  ♥attachments = ♥email⟦attachments⟧
  foreach ♥attachment in ♥attachments
    dialog ‴File attached: ♥attachment⟦filename⟧, size: ♥attachment⟦size⟧ bytes‴
  end
end
```

Note that two other Outlook structures are used here as well: [outlookfolder](outlookfolderstructure.md) (for the `♥InboxFolder` variable) and [outlookmail](outlookmailstructure.md) (for the `♥email` variable).