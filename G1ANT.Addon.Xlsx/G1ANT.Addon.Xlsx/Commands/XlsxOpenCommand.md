# xlsx.open

**Syntax:**

```G1ANT
xlsx.open  path ‴‴  accessmode ‴‴
```

**Description:**

Command `xlsx.open` allows to open .xlsx files, and set the first sheet in the document as active.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`path`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes|  | path of file that has to be opened|
|`accessmode`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes|  | ‴read‴ or ‴readwrite‴ |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no |  [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of variable where command's result will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Xlsx.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Xlsx](https://github.com/G1ANT-Robot/G1ANT.Addon.Xlsx)

This command is contained in **G1ANT.Addon.Xlsx.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Xlsx](https://github.com/G1ANT-Robot/G1ANT.Addon.Xlsx)

**Example 1:**

Here document.xlsx is being opened and result is assigned to the variable called ♥excelId1.

```G1ANT
xlsx.open path ‴C:\programs\document.xlsx‴ result ♥excelId1
```

**Example 2:**

In this example while the `accessmode` argument is set to 'read' G1ANT.Robot will only open the file without posibility to modify it.

```G1ANT
xlsx.open path ‴C:\Tests\test.xlsx‴ accessmode ‴read‴
```

**Example 3:**

G1ANT.Robot will open the file and can modify it.

```G1ANT
 xlsx.open path ‴C:\Tests\test.xlsx‴ accessmode ‴readwrite‴
```
