# outlookfolder

This structure stores information about the Outlook folder, which was retrieved with the `outlook.getfolder` command. It contains the following fields:

| Field        | Type                                                        | Description                                     |
| ------------ | ----------------------------------------------------------- | ----------------------------------------------- |
| `name`       | [text](../../../g1ant.language/structures/textstructure.md) | The name of the folder                          |
| `folderpath` | [text](../../../g1ant.language/structures/textstructure.md) | The path to the folder                          |
| `folders`    | [list](../../../g1ant.language/structures/liststructure.md) | The list of subfolders                          |
| `mails`      | [list](../../../g1ant.language/structures/liststructure.md) | The list of email messages stored in the folder |
| `unreaded`   | [list](../../../g1ant.language/structures/liststructure.md) | The list of unread messages                     |

## Example

The script below retrieves unread emails from the Outlook Inbox folder, using the `♥InboxFolder` variable, which is of the `outlookfolder` structure (be sure to provide the correct Outlook folder information in the `♥outlookInboxFolder` variable):

```G1ANT
♥outlookInboxFolder = john.doe@g1ant.com\Inbox

outlook.open display false
outlook.getfolder ♥outlookInboxFolder result ♥InboxFolder errormessage ‴Cannot find folder "♥outlookInboxFolder"‴
♥unreademails = ♥InboxFolder⟦unreaded⟧
foreach ♥email in ♥unreademails
  dialog ‴New message from ♥email⟦from⟧ with subject: "♥email⟦subject⟧"‴
end
```

Note that another Outlook structure is used here as well: [outlookmail](outlookmailstructure.md) (for the `♥email` variable).