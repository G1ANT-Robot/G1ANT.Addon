# googlesheet.switch

**Syntax:**

```G1ANT
googlesheet.switch  id ‴‴
```

**Description:**

Command `googlesheet.switch` allows to switch between opened Google Sheets instances.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`id`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | title of Google Sheets instance that will be activated |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of variable where command's result will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.GoogleDocs.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.GoogleDocs](https://github.com/G1ANT-Robot/G1ANT.Addon.GoogleDocs)

**Example 1:**

```G1ANT
googlesheet.open id ‴1gKFnrtZ-kzijNeIpYxln6PZS0z5btyHjoW1vZhCZ58c‴ result ♥sheetHandle1
googlesheet.gettitle result ♥title
dialog ♥title
googlesheet.open id ‴17LyNRs5K0St74MMnBDSX4hrzACjGfddOokNM_3hS4sg‴ result ♥sheetHandle2
googlesheet.gettitle result ♥title
dialog ♥title
googlesheet.switch id ♥sheetHandle1
googlesheet.gettitle result ♥title
dialog ♥title
googlesheet.close id ♥sheetHandle1 timeout 10000
```

In order to use `googlesheet.switch` command you need to have at least two Google Sheets opened. You switch between them by assigning a `result` argument to them while using `googlesheet.open` command and then using it as value for an `id` argument in `googlesheet.switch`
