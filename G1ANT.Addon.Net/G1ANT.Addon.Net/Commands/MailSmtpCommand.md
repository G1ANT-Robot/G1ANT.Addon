# mail.smtp

**Syntax:**

```G1ANT
mail.smtp  login ‴‴  password ‴‴ from ‴‴
```

**Description:**

Command `mail.smtp` allows sending mail from specified email address to specified receiver.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`login`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  |login of user who is sending the email|
|`password`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  |password of user who is sending the email|
|`from`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  |sender's email address|
|`port`| "int":{TOPIC-LINK+integer}| no | 587 |smtp client port|
|`host`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  smtp.gmail.com |smtp client host name|
|`to`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  |receiver's email address|
|`subject`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no|  |mail subject|
|`body`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no|  |mail body, main content of the email |
|`attachments`| [list](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/list.md)  | no |  | array of full paths to all attached files|
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutmailsmtp](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Net.dll**.
` See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Net](https://github.com/G1ANT-Robot/G1ANT.Addon.Net)

**Example 1:**

```G1ANT
mail.smtp login ‴test‴ password ‴testpass123‴ host ‴test.com‴ port 25 from ‴test‴ to
‴test2`test.com‴ subject ‴something‴ body ‴testtest‴ attachments ‴C:\tests\test.png‴
```
