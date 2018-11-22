# excel.importtext

**Syntax:**

```G1ANT
excel.importtext path ‴‴
```

**Description:**

Command `excel.importtext` allows to set a data connection between text file and the specified destination in an active sheet and import data into it.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`path`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes | | path of a file that has to be imported (csv data format is supported) |
|`destination`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no | A1 | top left cell area of imported data, specified as either string or point |
|`delimiter`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no | semicolon | delimiter to be used while importing data, accepts 'tab', 'semicolon', 'comma', 'space' or any other character |
|`name`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no|  | range name where data will be placed|
|`resultrows`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | ♥resultrows | name of variable where size of imported data, string formated as ‴#rows,#columns‴ will be stored |
|`resultcolumns`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | ♥resultcolumns | name of variable where size of imported data, string formated as ‴#rows,#columns‴ will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no |  | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.MSOffice.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice](https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice)

**Example 1:**

In these examples data from the data.csv file is imported, copying takes place in cell D5 (or, in the other notation, cell 4//5).

```G1ANT
excel.importtext path ‴C:\programs\data.csv‴ destination ‴D5‴ result ♥size
excel.importtext path ‴C:\programs\data.csv‴ destination ‴4//5‴ result ♥size
```

**Example 2:**

```G1ANT
excel.open result ♥excelHandle
excel.importtext path ‴C:\Tests\import_random.csv‴ delimiter comma resultrows ♥importerRows destination ‴B1‴ resultcolumns ♥importedColumns
dialog ‴Rows imported: ♥importerRows; Columns imported: ♥importedColumns‴
excel.save path ‴C:\Tests\import_test.xlsx‴
excel.close
```

