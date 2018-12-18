# excel.getrow

**Syntax:**

```G1ANT
excel.getrow  row ‴‴
```

**Description:**

Command `excel.getrow` gets all used cells of the specified row.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`row`| [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md) | yes |  | cell's row number |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no |  [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of variable where command's result will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.MSOffice.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice](https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice)

**Example:**

```G1ANT
excel.open
window title ‴✱Excel‴
keyboard ‴THE wild bee reels from bough to bough ⋘DOWN⋙With his furry coat and his gauzy wing. ⋘DOWN⋙Now in a lily-cup, and now THE wild bee reels from bough to bough ⋘DOWN⋙Setting a jacinth bell a-swing,  ⋘DOWN⋙In his wandering; ⋘DOWN⋙Sit closer love: it was here I trow ⋘DOWN⋙I made that vow⋘DOWN⋙With his furry coat and his gauzy wing. ,‴
keyboard ⋘ENTER⋙
excel.getrow row ‴3‴ result ♥rowInput
dialog ♥rowInput
```
