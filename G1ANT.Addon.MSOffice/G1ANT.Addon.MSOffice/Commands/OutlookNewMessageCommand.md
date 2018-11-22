# outlook.newmessage

**Syntax:**

```G1ANT
outlook.newmessage
```

**Description:**

Command `outlook.newmessage` allows to open a new message window and fills it up with provided arguments.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`to`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | mail recipients |
|`subject`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | mail subject |
|`body`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | mail body |
|`attachments`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | list of attachments to be included in mail, should be separated by ❚ |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of variable where execution status will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.MSOffice.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice](https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice)

**Example 1:**

Here an email will be drafted with all main fields completed, with no attachments.

```G1ANT
outlook.newmessage to ‴mail`g1ant.com‴ subject ‴amazing‴ body ‴it is an amazing feature‴
```

**Example 2:**

Here an email will be drafted with all main fields completed and with two attachments.

```G1ANT
outlook.newmessage to ‴mail`g1ant.com‴ subject ‴amazing‴ body ‴it is an amazing feature‴ attachments ‴c:\Temp\Text.txt❚c:\Temp\Text1.txt‴
```

**Example 3 :**

Here an email will be drafter with all main fields completed with one attachment.

```G1ANT
outlook.newmessage to ‴mail`g1ant.com‴ subject ‴amazing‴ body ‴this is an amazing feature‴ attachments ‴c:\Temp\Text.txt‴
```

**Example 4:**

```G1ANT
outlook.open
outlook.newmessage to ‴test`g1ant.com‴ subject ‴something interesting‴ body ‴sth sth sth‴
outlook.close
```
