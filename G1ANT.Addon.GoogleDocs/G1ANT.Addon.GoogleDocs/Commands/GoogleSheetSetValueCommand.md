# googlesheet.setvalue

**Syntax:**

```G1ANT
googlesheet.setvalue  cell ‴‴  value ‴‴
```

**Description:**

Command `googlesheet.setvalue` allows to set value in opened Google Sheets instance.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`cell`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md)  | yes |  | cell name (like A6) where you want to inject data |
|`value`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md)  | yes | | new value to be inserted inside of a chosen cell |
|`sheetname`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md)  | no |  | sheet name where range exists, can be empty or omitted |
|`numeric`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md)  | no | true | determines if new value should be inserted as numeric or not |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.GoogleDocs.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.GoogleDocs](https://github.com/G1ANT-Robot/G1ANT.Addon.GoogleDocs)

**Example 1:**

```G1ANT
googlesheet.open id ‴1gKFnrtZ-kzijNeIpYxln6PZS0z5btyHjoW1vZhCZ58c‴ result ♥sheetHandle
googlesheet.setvalue cell ‴A2‴ value ‴525‴ numeric true
googlesheet.close id ♥sheetHandle timeout 10000
```

`googlesheet.setvalue` command lets you inject certain 'value' inside of a chosen cell. In order to use it, you first need to open a google spreadsheet using `googlesheet.open`.
You can assign it to a variable with `result` argument to be able to reuse it.
Next, choose a `googlesheet.setvalue` command and fill `cell`, `value` and `numeric` arguments with proper values. In our example G1ANT.Robot will inject '525' as a number  into the cell - because we set `numeric` argument to true.

**Example 2:**

```G1ANT
googlesheet.open id ‴1gKFnrtZ-kzijNeIpYxln6PZS0z5btyHjoW1vZhCZ58c‴ result ♥sheetHandle
googlesheet.setvalue cell ‴A2‴ value ‴Random‴ numeric false
googlesheet.close id ♥sheetHandle timeout 10000
```

See our example below, this time we typed 'Random',  which is a text (`numeric false`), inside of A2 cell.

