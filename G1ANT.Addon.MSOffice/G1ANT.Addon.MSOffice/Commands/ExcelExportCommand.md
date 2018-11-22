# excel.export

**Syntax:**

```G1ANT
excel.export path ‴‴ 
```

**Description:**

Command `excel.export` exports currently active excel workbook to either **.pdf or **.xps file.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`path`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | path where new file will be stored|
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no |  | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.MSOffice.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice](https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice)

**Example 1:**

In both examples below a currently active excel sheet is being exported as a .pdf file to C:\Documents\FileName.pdf

```G1ANT
excel.export path ‴C:\Documents\FileName1.pdf‴
```

```G1ANT
excel.export path ‴FileName2.pdf‴ type ‴pdf‴
```

**Example 2:**

It is very important that you type the file extension while giving the path value. In our example 'startrek.pdf' or 'startrek.xlsx'.

```G1ANT
excel.open path ‴C:\Tests\startrek.xlsx‴ result ♥excelHandle
excel.export path ‴C:\Tests\startrek.pdf‴
excel.close
```
