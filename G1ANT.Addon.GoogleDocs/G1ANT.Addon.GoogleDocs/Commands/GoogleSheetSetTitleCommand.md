# googlesheet.settitle

**Syntax:**

```G1ANT
googlesheet.settitle  title ‴‴ 
```

**Description:**

Command `googlesheet.settitle` allows to set a title of a Google Sheet file.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`title`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | new spreadsheet title |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.GoogleDocs.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.GoogleDocs](https://github.com/G1ANT-Robot/G1ANT.Addon.GoogleDocs)

**Example 1:**

`googlesheet.settitle` changes the title of Google Sheet document. Following the script below you can see than in order to change the title, you first need to open a file. `googlesheet.gettitle` is an additional command that we choose to see the current title.
`googlesheet.settitle` demands `title` argument, which is a string- new value of the title that G1ANT.Robot will type in the title field.

```G1ANT
googlesheet.open id ‴1gKFnrtZ-kzijNeIpYxln6PZS0z5btyHjoW1vZhCZ58c‴ result ♥sheetHandle
googlesheet.gettitle result ♥title
dialog ♥title
googlesheet.settitle title ‴Test2017‴
googlesheet.gettitle result ♥title
dialog ♥title
googlesheet.close id ♥sheetHandle timeout 10000
```

The title of our Google Sheet was 'KingOfTheTests'

Changing the title of a Google Sheet document to 'Test2017' using `googlesheet.settitle` command

