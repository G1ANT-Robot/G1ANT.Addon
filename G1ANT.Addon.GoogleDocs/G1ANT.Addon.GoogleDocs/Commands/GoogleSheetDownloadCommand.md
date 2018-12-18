# googlesheet.download

**Syntax:**

```G1ANT
googlesheet.download  path ‴‴
```

**Description:**

Command `googlesheet.download` allows to download the whole spreadsheet.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`path`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | destination on your computer where the file will be saved |
|`type`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no | xlsx | type of file extension, could be '.pdf' or '.xlsx' |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of variable where command's result will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.GoogleDocs.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.GoogleDocs](https://github.com/G1ANT-Robot/G1ANT.Addon.GoogleDocs)

**Example 1:**

```G1ANT
googlesheet.open id ‴1w5iopoKzgALxC1Qumtzvmc4VkXPq6kgkxieISibBpTs‴ result ♥sheetHandle
googlesheet.download path ‴C:\tests\file.xlsx‴ type ‴xlsx‴
googlesheet.close id ♥sheetHandle
```

To use `googlesheet.download` command, you first need to open the file that you want to download the path from. While opening, you need to give the id of the file. You can find the id here:

`googlesheet.download` will save this file to a chosen path on your computer. `path` argument expects a place where the file should be downloaded, `type` argument expects either 'xlsx' or 'pdf' value for file extension.
Remember that while choosing a path, you need to create the name of the file with certain extension- ‴C:\tests\file.xlsx‴ - in this case 'file.xlsx'. G1ANT.Robot will create an Excel file.

**Example 2:**

G1ANT.Robot will create a pdf file.

```G1ANT
googlesheet.open id ‴1w5iopoKzgALxC1Qumtzvmc4VkXPq6kgkxieISibBpTs‴ result ♥sheetHandle
googlesheet.download path ‴C:\tests\file.pdf‴ type ‴pdf‴
googlesheet.close id ♥sheetHandle
```
