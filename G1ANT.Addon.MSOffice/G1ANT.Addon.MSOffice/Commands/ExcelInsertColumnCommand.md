# excel.insertcolumn

**Syntax:**

```G1ANT
excel.insertcolumn  colindex ‴‴
```

or

```G1ANT
excel.insertcolumn  colname ‴‴
```

**Description:**

Command `excel.insertcolumn` inserts empty column in specified place.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`colindex` or `colname`|  [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md)  or [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | `colindex` - cell's column number, `colname` - cell's column name |
|`where`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no | after | determines, whether to insert column 'before' or 'after' specified column |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump`| [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no |  | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no | | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.MSOffice.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice](https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice)

**Example 1:**

This example opens an empty Excel instance and inserts a column after column A.

```G1ANT
excel.open
excel.setvalue value ‴someting‴ row 1 colindex 1
excel.setvalue value ‴someting else‴ row 1 colindex 2
excel.insertcolumn colname A where ‴after‴
```

**Example 2:**

This example opens an existing Excel instance from a path and inserts a column before column A.

```G1ANT
excel.open path ‴C:\Tests\test.xlsx‴ result ♥excelHandle
excel.insertcolumn colname A where ‴before‴
```
