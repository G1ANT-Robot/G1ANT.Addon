# excel.open

**Syntax:**

```G1ANT
excel.open
```

**Description:**

Command `excel.open` allows to open a new Excel instance.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`path`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | path of a file that has to be opened, if not specified, excel will be opened anyway |
|`inbackground`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | false | defines whether Excel opens in the background  |
|`sheet`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | sheet name to be activated |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of variable where number of currently opened Excel processes is stored, it can be used later on with command `excel.switch` |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump`| [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no |  | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.MSOffice.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice](https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice)

**Example 1:**

Here document.xlsx is being open with activated sheet_number_223 and the result is assigned to ♥excelId1 variable.

```G1ANT
excel.open path ‴C:\programs\document.xlsx‴ sheet ‴sheet_number_223‴ result ♥excelId1
```

**Example 2:**

Below you can see how easy it is to open a new Excel file:

```G1ANT
excel.open
```

**Example 3:**

If you choose that `excel.open` command works in background, you will not notice any action, but G1ANT.Robot will perform according to the script.

```G1ANT
excel.open inbackground true
excel.setvalue value ‴Random Text‴ row 1 colname A
excel.save path ‴C:\Tests\test.xlsx‴
excel.close
```

**Example 4:**

In this example you can create Sheet2 while opening Excel.

```G1ANT
excel.open path ‴C:\Tests\test.xlsx‴ sheet Sheet2
```
