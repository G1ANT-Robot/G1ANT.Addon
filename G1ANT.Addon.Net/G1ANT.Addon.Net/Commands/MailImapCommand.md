# mail.imap

## Syntax

```G1ANT
mail.imap host ‴‴ port ‴‴ login ‴‴ password ‴‴ sincedate ⟦date⟧ todate ⟦date⟧ onlyunreadmessages ⟦bool⟧ markallmessagesasread ⟦bool⟧ ignorecertificateerrors ⟦bool⟧ result ⟦mail⟧♥result
```

## Description

This command uses the IMAP protocol to check an email inbox and allows the user to analyze their messages received within a specified time range, with the option to consider only unread messages and/or mark all of the checked ones as read. The result of the command is a list of mail variables — please refer to [mail structure](../../G1ANT.Language/Structures/mailstructure.md) to see what elements are stored in. it.

| Argument               | Type                                                         | Required | Default Value                                                | Description                                                  |
| ---------------------- | ------------------------------------------------------------ | -------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| `host`                 | [text](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes      |                                                              | IMAP server address                                          |
| `port`                 | [text](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes      |                                                              | IMAP server port number                                      |
| `login`                | [text](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes      |                                                              | User email login                                             |
| `password`             | [text](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes      |                                                              | User email password                                          |
| `sincedate`            | [date]()                                                     | no       |                                                              | Starting date for messages to be checked                     |
| `todate`               | [date]()                                                     | no       |                                                              | Ending date for messages to be checked                       |
| `onlyunreadmessages`   | [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no       |                                                              | If set to `true`, only unread messages will be checked       |
| `markallmessagesasread`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no       |                                                              | If set to `true`, all checked messages will be marked as read |
| `ignorecertificateerrors` | [bool]() | no       |                                                              | If set to `true`, the command will ignore any security certificate errors |
| `result`               | [variable]()                                                     | no       | [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md) | The name of a list variable where the returned [mail](../../G1ANT.Language/structures/mailstructure.md) variables will be stored |

### Example

```G1ANT
♥dateformat = dd\mm\yyyy
♥yesterday = ⟦date⟧13.01.2018
mail.imap host imap.gmail.com port 993 login mail@gmail.com password p@$$w0rD sincedate ♥yesterday todate ♥date onlyunreadmessages true markallmessagesasread false result ♥list 

foreach ♥element in ♥list
  dialog ♥element
end foreach
```

In the example above the `mail.imap` command will check GMail IMAP server for unread messages received between January 13, 2018 and the present moment. The matching emails will be displayed in the subsequent dialog boxes (only message subjects are shown, because it’s a default behavior of the mail structure).

**Note:** Host, login and password values are of course just examples. You have to provide your real mail server credentials for the command to work.
