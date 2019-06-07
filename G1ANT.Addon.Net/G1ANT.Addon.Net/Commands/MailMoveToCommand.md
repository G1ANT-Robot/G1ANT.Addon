# mail.moveto

## Syntax

```G1ANT
mail.moveto host ⟦text⟧ port ⟦integer⟧ login ⟦text⟧ password ⟦text⟧ mail ⟦mail⟧ sincedate ⟦date⟧ todate ⟦date⟧ onlyunreadmessages ⟦bool⟧ markallmessagesasread ⟦bool⟧ ignorecertificateerrors ⟦bool⟧
```

## Description

This command moves a selected message to another folder.

| Argument            | Type                                                         | Required | Default Value                                               | Description                                                  |
| ------------------- | ------------------------------------------------------------ | -------- | ----------------------------------------------------------- | ------------------------------------------------------------ |
| `host`                 | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes      |                                                              | IMAP server address                                          |
| `port`                 | [integer](G1ANT.Language/G1ANT.Language/Structures/IntegerStructure.md) | yes      |                                                              | IMAP server port number                                      |
| `login`                | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes      |                                                              | User email login                                             |
| `password`             | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes      |                                                              | User email password                                          |
| `mail` | [mail](G1ANT.Language/G1ANT.Language/Structures/MailStructure.md) | yes |  | Mail message to be moved                                     |
| `folder`            | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes   |                                                              | Name of the destination folder |
| `ignorecertificateerrors` | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       |                                                              | If set to `true`, the command will ignore any security certificate errors |
| `if`                | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`           | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimespanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`         | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`         | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage`      | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md)  | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`       | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

In the example below the `mail.imap` command will check Gmail IMAP server for unread messages in the default INBOX folder, received between January 13, 2019 and the present moment. The matching emails will be moved to Spam folder in the same mail account.

> **Note:** Host, login and password values are of course just examples. You have to provide your real mail server credentials for the command to work.

```G1ANT
♥yesterday = ⟦date:dd.MM.yyyy⟧13.01.2019
mail.imap imap.gmail.com login mail@gmail.com password p@$$w0rD sincedate ♥yesterday todate ♥date onlyunreadmessages true markallmessagesasread false ignorecertificateerrors true result ♥list 

foreach ♥element in ♥list
  mail.moveto imap.gmail.com login mail@gmail.com password p@$$w0rD mail ♥element folder Spam
end
```
