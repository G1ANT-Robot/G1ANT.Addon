# excel.getformula

**Syntax:**

```G1ANT
excel.getformula  row ‴‴ colindex ‴‴
```

or

```G1ANT
excel.getformula  row ‴‴ colname ‴‴
```

**Description:**

Command `excel.getformula` allows to get formula from specified cell.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`row`| [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md) | yes |  | cell's row number |
|`colindex` or `colname`|  [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md)  or [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | `colindex` - cell's column number, `colname` - cell's column name |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)   | name of variable where command's result will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no |  | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.MSOffice.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice](https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice)

**Example 1:**

```G1ANT
excel.getformula row 11 colindex 3
dialog  ♥result
```

**Example 2:**

```G1ANT
excel.getformula row 11 colname ‴3‴ result ♥var1
dialog  ♥var1
```

In this example formula used in the specified cell is shown in a dialog.

**Example 3:**

```G1ANT
excel.open path ‴C:\Tests\test.xlsx‴
excel.activatesheet name ‴Sheet3‴
excel.getformula row 1 colname ‴A‴ result ♥formula
dialog ♥formula
```
