# xlsx.close

**Syntax:**

```G1ANT
xlsx.close
```

**Description:**

Command `xlsx.close` allows to save changes and close .xlsx and .xls file extensions.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`id`| [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md)  | no |  | id of file to close, if not set, will close file opened as first|
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Xlsx.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Xlsx](https://github.com/G1ANT-Robot/G1ANT.Addon.Xlsx)

**Example 1:**

Document with .xlsx file extension, which was opened as first one, is now being closed.

```G1ANT
xlsx.close
```

**Example 2:**

Here document with .xlsx file extension with ID '444' is being closed.

```G1ANT
xlsx.close id ♥444
```

**Example 3:**

```G1ANT
xlsx.open path ‴C:\Tests\Book1.xlsx‴ result ♥xlsHandle
xlsx.open path ‴C:\Tests\Book2.xlsx‴ result ♥xlsHandle2
xlsx.close id ♥xlsHandle
```

The command is called `xlsx.close` but can work both on .xls and .xlsx file extensions.
