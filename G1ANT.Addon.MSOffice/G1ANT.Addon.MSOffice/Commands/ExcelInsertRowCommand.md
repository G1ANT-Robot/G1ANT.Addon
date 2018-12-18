# excel.insertrow

**Syntax:**

```G1ANT
excel.insertrow  row ‴‴
```

**Description:**

Command `excel.insertrow` inserts empty row in specified place.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`row`| [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md) | yes |  | row's number |
|`where`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no | below | determines, whether to insert row 'below' or 'above' specified row |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump`| [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no |  | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no | | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.MSOffice.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice](https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice)

**Example 1:**

```G1ANT
excel.insertrow row 2 where below
```

**Example 2:**

```G1ANT
excel.insertrow row 1 where above
```

**Example 3:**

```G1ANT
excel.insertrow row 1
```

**Example 4:**

```G1ANT
excel.open
keyboard ‴Remember, remember!‴
keyboard ⋘DOWN⋙
keyboard ‴the fifth of November⋘DOWN⋙The Gunpowder treason and plot‴
keyboard ⋘ENTER⋙
delay seconds 4
excel.insertrow row 2 where below
dialog ‴row inserted‴
```

**Example 5:**

```G1ANT
excel.open path ‴C:\tests\test.xlsx‴ result ♥excel1
excel.insertrow row 3 where before
```
