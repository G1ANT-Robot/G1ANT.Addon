# xlsx.setsheet

**Syntax:**

```G1ANT
xlsx.setsheet
```

**Description:**

Command `xlsx.setsheet` allows to set active sheet to work with.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`name`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |   | name of sheet we want to work with. If not set, the robot will get first sheet in the file |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no |  [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of variable where command's result will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Xlsx.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Xlsx](https://github.com/G1ANT-Robot/G1ANT.Addon.Xlsx)

**Example 1:**

Here, the first sheet in file will be set as active.

```G1ANT
xlsx.open path ‴C:\Tests\Book1.xlsx‴ result ♥xls2
xlsx.setsheet
```

**Example 2:**

Here, the sheet with name 'Sheet3' will be set as active.

```G1ANT
xlsx.open path ‴C:\Tests\Book1.xlsx‴ result ♥xls2
xlsx.setsheet name ‴Sheet3‴
```

**Example 3:**

In this example G1ANT.Robot will open Excel document from a path. `xlsx.setsheet` command will make the sheet named 'StarWars' active. It will not create a new one. To check whether G1ANT.Robot made the proper sheet active, we can dialog the value of cell A1. It will show '1'.

```G1ANT
xlsx.open path ‴C:\Tests\Book1.xlsx‴ result ♥xls2
xlsx.setsheet name ‴StarWars‴
xlsx.getvalue position ‴A1‴ result ♥A1
dialog ♥A1
```

 
