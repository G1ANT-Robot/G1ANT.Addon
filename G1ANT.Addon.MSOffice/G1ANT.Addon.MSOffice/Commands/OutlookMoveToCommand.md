# outlook.moveto

## Syntax

```G1ANT
outlook.moveto item ⟦outlookmail/outlookfolder⟧ destinationfolder ⟦outlookfolder⟧
```

## Description

This command is used to move an individual email message or a whole folder to another location (Outlook folder).

| Argument            | Type                                                         | Required | Default Value                                                | Description                                                  |
| ------------------- | ------------------------------------------------------------ | -------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| `item`              | [outlookmail](../../../G1ANT.Language/Structures/OutlookMailStructure.md) or [outlookfolder](../../../G1ANT.Language/Structures/OutlookFolderStructure.md) | yes      |                                                              | An item (a message or a folder) to be moved                  |
| `destinationfolder` | [outlookfolder](../../../g1ant.language/structures/outlookfolderstructure.md) | yes      |                                                              | Destination Outlook folder                                   |
| `if`           | [bool](../../../g1ant.language/structures/booleanstructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](../../../g1ant.language/structures/timespanstructure.md) | no       | [♥timeoutcommand](../../../appendices/special-variables.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](../../../g1ant.language/structures/procedurestructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](../../../g1ant.language/structures/labelstructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](../../../g1ant.language/structures/textstructure.md)  | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](../../../G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](../../../G1ANT.Language/Structures/ErrorStructure.md) structure   |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](../../../appendices/common-arguments.md) page.

## Example

In the example below, Outlook opens silently in the background, its source and destination folders are retrieved to variables with the `outlook.getfolder` commands, then the first email message from the Inbox folder is moved to the destination folder (be sure to provide the correct Outlook folder information in the `♥outlookInboxFolder` and `♥outlookMoveToFolder` variables):

```G1ANT
♥outlookInboxFolder = john.doe@g1ant.com\Inbox
♥outlookMoveToFolder = john.doe@g1ant.com\[Gmail]\New

outlook.open display false

outlook.getfolder ♥outlookInboxFolder result ♥inboxFolder errormessage ‴Cannot find folder "♥outlookInboxFolder"‴
outlook.getfolder ♥outlookMoveToFolder result ♥moveToFolder errormessage ‴Cannot find folder "♥outlookMoveToFolder"‴
♥emails = ♥inboxFolder⟦mails⟧
outlook.moveto item ♥emails⟦1⟧ destinationfolder ♥moveToFolder
```
