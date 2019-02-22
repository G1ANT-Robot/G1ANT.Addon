# mail.smtp

## Syntax

```G1ANT
mail.smtp host ⟦text⟧ port ⟦integer⟧ login ⟦text⟧ password ⟦text⟧ from ⟦text⟧ to ⟦text⟧ cc ⟦text⟧ bcc ⟦text⟧ subject ⟦text⟧ body ⟦text⟧ ishtmlbody ⟦bool⟧ attachments ⟦list⟧
```

## Description

This command sends a mail message from a provided email address to a specified recipient.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
| `host`                 | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes      |                                                              | SMTP server address                                          |
| `port`                 | [integer](G1ANT.Language/G1ANT.Language/Structures/IntegerStructure.md) | yes      | 587 | SMTP server port number                                   |
| `login`                | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes      |                                                              | User email login                                             |
| `password`             | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes      |                                                              | User email password                                          |
|`from`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  |Sender's email address|
|`to`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  |Recipient's email address|
| `cc`           | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes      |                                                              | Carbon copy address(es); use semicolon (;) to separate multiple addresses |
|`bcc`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes | |Blind carbon copy address(es); use semicolon (;) to separate multiple addresses|
|`subject`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no | |Message subject|
|`body`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no|  |Message body, i.e. the main content of an email |
|`attachments`| [list](G1ANT.Language/G1ANT.Language/Structures/ListStructure.md) | no |  | List of full paths to all files to be attached |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutmailsmtp](G1ANT.Addon/G1ANT.Addon.Net/G1ANT.Addon.Net/Variables/TimeoutMailSmtpVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

In the following script the `mail.smtp` command will send a message via Gmail SMTP server from a sender named Robot to a recipient whose email address is `hi@g1ant.com`.  The message subject is “*Test*”, the content is just “*Hi, G1ANT!*” and one file, *hello.jpg*, is attached.

> **Note:** Host, login and password values are of course just examples (and so is the .jpg file to be attached). You have to provide your real mail server credentials for the command to work.

```G1ANT
list.create C:\photos\hello.jpg result ♥attachment
mail.smtp imap.gmail.com login mail@gmail.com password p@$$w0rD from robot@gmail.com to hi@g1ant.com subject Test body ‴Hi, G1ANT!‴ attachments ♥attachment
```
