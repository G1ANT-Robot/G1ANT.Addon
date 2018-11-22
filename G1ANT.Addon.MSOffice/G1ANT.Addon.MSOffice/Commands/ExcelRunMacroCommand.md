# excel.runmacro

**Syntax:**

```G1ANT
excel.runmacro  name ‴‴
```

**Description:**

Command `excel.runmacro` allows to run macro in currently active excel program.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`name`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | name of macro that is defined in a workbook |
|`args`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no| | comma separated arguments that will be passed to macro |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no |  [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of variable where macro's return value will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.MSOffice.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice](https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice)

**Quick Excel set up before we start with macros:**

**Example 1:**

The first example will multiply values from one column by 2 and inserts results in a second column. Please find this "excel-macros.xlsx":{DOCUMENT-LINK+excel-macros} file and download it. There you can see two columns: "Numbers" (which contain some random values) and "Results of Multiplication" (which will contain results of multiplication once we have run the macro).

The easiest way to add a macro is to record a macro.

Please click "Record Macro" (make sure you have Developer tab opened), specify some name for your macro for example "Multiplication", focus B2 cell, insert "=" and click A2 cell, insert "**2", click "enter" and then expand values from B2 cell to B6 in order to have all result values in a B column. Finally, click "Stop Recording" in "Code" section.

Now whenever you want, you can click "Macros" in "Code" section and click "run" so you will have those results inserted and updated even when you change values in an A column. Please, clear contents of B2:B6 cells for further purpose.

Let's try the second way of adding a macro.

Please open "Visual Basic" in "Code" section, expand "Modules", double click "Module 1"  and here you should have your generated code inserted depending on how you have recorded your macro. I have such code:

```G1ANT
Sub Multiplication()
'
' Multiplication Macro
'
'
    Range("B2").Select
    ActiveCell.FormulaR1C1 = "=RC[-1]**2"
    Range("B2").Select
    Selection.AutoFill Destination:=Range("B2:B6"), Type:=xlFillDefault
    Range("B2:B6").Select
End Sub
```

Note, that there are two types of macros that you can run - "Function" which returns some value and "Sub" which may perform some action in Excel.
We've had Sub Multiplication(), so let's add another type of macro  this time - "Function" which returns some value. Please, paste the code below to try it out:

```G1ANT
Function test() As Single
    test = 10
End Function
```

This will return value of test. In order to see if it worked, let's add another Sub which will print value of test into a message box:

```G1ANT
Sub Message()
MsgBox ("Some example number: " + CStr(test()))
End Sub
```

Let's save it, close Microsoft Visual Basic and save Excel under the ".xlsm" extension (so that it supports macros) and close Excel.

Please, open G1ANT.Robot, paste this code and run it:

```G1ANT
excel.open ‴C:\Users\a\Desktop\Wiktoria\Macro-Examples.xlsm‴
window ✱Excel
excel.runmacro ‴Multiplication‴
excel.runmacro ‴Test‴ result ♥macroResult
excel.runmacro ‴Message‴
dialog ♥macroResult
```

Make sure that the path to your excel file is accurate after the excel.open command.
excel.runmacro command may have the result argument specified when macro that it runs is a Function one, not a Sub one.
