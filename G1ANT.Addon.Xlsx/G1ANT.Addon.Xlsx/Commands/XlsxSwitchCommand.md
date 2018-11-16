# xlsx.switch

**Syntax:**

```G1ANT
xlsx.switch  id ‴‴
```

**Description:**

Command `xlsx.switch` allows to switch between opened .xlsx files.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`id`| [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md)  | yes |  | ID of opened file we want to work with |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no |  [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of variable where command's result will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Xlsx.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Xlsx](https://github.com/G1ANT-Robot/G1ANT.Addon.Xlsx)

**Example 1:**

In order to use `xlsx.switch` command, first open two .xls files  with `xlsx.open` and assign different values for `result` argument in order to use them as `id` later. It is essential that while using `xlsx.switch` you give `id` argument as it is required for this command.

```G1ANT
xlsx.open path ‴C:\Tests\tests.xlsx‴ accessmode ‴read‴ result ♥xls1
xlsx.open path ‴C:\Tests\Book1.xlsx‴ accessmode ‴readwrite‴ result ♥xls2
xlsx.switch id ♥xls1
xlsx.getvalue position ‴A1‴ result ♥A1
dialog ♥A1
xlsx.switch id ♥xls2
xlsx.getvalue position ‴A1‴ result ♥A2
dialog ♥A2
```
