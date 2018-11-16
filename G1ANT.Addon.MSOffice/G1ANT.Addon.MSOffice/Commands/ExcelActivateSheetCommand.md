# excel.activatesheet

**Syntax:**

```G1ANT
excel.activatesheet  name ‴‴
```

**Description:**

Command `excel.activatesheet` allows to activate sheet in currently active excel instance.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`name`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | sheet name to be activated|
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no |  | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.MSOffice.dll.**
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice](https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice)

**Example 1:**

In this example there is a change of active switch - instead of the previously active "sheet_number_223" sheet we activate "books1" sheet.

```G1ANT
excel.activatesheet name ‴books1‴
```

**Example 2:**

This example shows how few Excel commands work together. The `excel.activatesheet` command requires `name` argument. You need to type the value of it- in our case 'New Sheet' precisely.

```G1ANT
excel.open result ♥excelHandle1
excel.addsheet name ‴New Sheet‴
excel.setvalue value ‴new Sheet‴ row 1 colindex 1
excel.addsheet name ‴Another Sheet‴
excel.setvalue value ‴132‴ row 1 colname ‴A‴
excel.activatesheet name ‴New Sheet‴
```
