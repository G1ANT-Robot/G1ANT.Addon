# mail.imap

## Syntax

```G1ANT
mail.imap host ⟦text⟧ port ⟦integer⟧ login ⟦text⟧ password ⟦text⟧ folder ⟦text⟧ sincedate ⟦date⟧ todate ⟦date⟧ onlyunreadmessages ⟦bool⟧ markallmessagesasread ⟦bool⟧ ignorecertificateerrors ⟦bool⟧
```

## Description

This command uses the IMAP protocol to check an email inbox and allows the user to analyze their messages received within a specified time span, with the option to consider only unread messages and/or mark all of the checked ones as read. The result of the command is a list of mail variables — please refer to [mail structure](G1ANT.Language/G1ANT.Language/Structures/MailStructure.md) to see what elements are stored in. it.

| Argument               | Type                                                         | Required | Default Value                                                | Description                                                  |
| ---------------------- | ------------------------------------------------------------ | -------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| `host`                 | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes      |                                                              | IMAP server address                                          |
| `port`                 | [integer](G1ANT.Language/G1ANT.Language/Structures/IntegerStructure.md) | yes      | 993 | IMAP server port number                                      |
| `login`                | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes      |                                                              | User email login                                             |
| `password`             | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes      |                                                              | User email password                                          |
| `folder` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes | INBOX | Folder to fetch emails from |
| `sincedate`            | [date](G1ANT.Language/G1ANT.Language/Structures/DateStructure.md) | no       |                                                              | Starting date for messages to be checked                     |
| `todate`               | [date](G1ANT.Language/G1ANT.Language/Structures/DateStructure.md) | no       |                                                              | Ending date for messages to be checked                       |
| `onlyunreadmessages`   | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | false | If set to `true`, only unread messages will be checked       |
| `markallmessagesasread`| [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true | If set to `true`, all checked messages will be marked as read |
| `ignorecertificateerrors` | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | false | If set to `true`, the command will ignore any security certificate errors |
| `result`               | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       | ♥result | Name of a list variable where the returned [mail](G1ANT.Language/G1ANT.Language/Structures/MailStructure.md) variables will be stored |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

### Example

```G1ANT
♥yesterday = ⟦date:dd.MM.yyyy⟧13.01.2019
mail.imap imap.gmail.com login mail@gmail.com password p@$$w0rD sincedate ♥yesterday todate ♥date onlyunreadmessages true markallmessagesasread false ignorecertificateerrors true result ♥list 

foreach ♥element in ♥list
  dialog ♥element
end
```

In the example above the `mail.imap` command will check Gmail IMAP server for unread messages in the default INBOX folder, received between January 13, 2019 and the present moment. The matching emails will be displayed in the subsequent dialog boxes (only message subjects are shown, because it’s a default behavior of the mail structure).

> **Note:** Host, login and password values are of course just examples. You have to provide your real mail server credentials for the command to work.
