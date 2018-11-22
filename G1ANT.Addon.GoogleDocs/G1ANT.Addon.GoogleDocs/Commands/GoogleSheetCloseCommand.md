# googlesheet.close

**Syntax:**

```G1ANT
googlesheet.close
```

**Description:**

Command `googlesheet.close` allows to close Google Sheets instance.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`id`| [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md)  | no |  | id of spreadsheet that we are closing, id can be saved in a variable while using `googlesheet.open` command |
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
googlesheet.close id ♥sheetHandle timeout 10000
```

In order to use `googlesheet.close`, open Google Sheets first using `googlesheet.open` command. Assign the path to the `result` argument so that it is stored in a variable.
`googlesheet.close` command expects to get the `id` of a Google Sheet that you want to close. The value of `id` has to be stored in a variable.

**Example 2:**

In this example G1ANT.Robot will close instance of Google Sheet recently used in the sript.

```G1ANT
googlesheet.open id ‴1w5iopoKzgALxC1Qumtzvmc4VkXPq6kgkxieISibBpTs‴
googlesheet.close
```
