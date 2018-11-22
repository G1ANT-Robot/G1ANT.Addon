# xlsx.find

**Syntax:**

```G1ANT
xlsx.find  value ‴‴
```

**Description:**

Command `xlsx.find` allows to find address of a cell where specified value is stored.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`value`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes  | | value to be searched |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no |  [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of variable where command's result will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Xlsx.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Xlsx](https://github.com/G1ANT-Robot/G1ANT.Addon.Xlsx)

**Example 1:**

This function will find the value "123" in .xlsx document and save the cell's address in variable 'cell'.

```G1ANT
xlsx.find value ‴123‴ result ♥cell
```

**Example 2:**

In this example G1ANT.Robot will search the document to find 'aaa' value. The result in the dialog box will be: 'C1'.

```G1ANT
xlsx.open path ‴C:\Tests\Book1.xlsx‴ result ♥xlsHandle
xlsx.setsheet name ‴Sheet1‴
xlsx.find value ‴aaa‴ result ♥number errormessage ‴Value not found‴
dialog ♥number
xlsx.close id ♥xlsHandle
```

