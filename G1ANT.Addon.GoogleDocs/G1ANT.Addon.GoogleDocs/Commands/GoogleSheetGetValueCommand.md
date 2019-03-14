# googlesheet.getvalue

## Syntax

```G1ANT
googlesheet.getvalue range ⟦text⟧ sheetname ⟦text⟧
```

## Description

This command gets a value from a specified cell or range in an opened Google Sheets instance.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`range`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Cell or range to get a value from, for example `A6`, `A3&B7` or `A3:A6&B7` |
|`sheetname`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no | | Sheet name containing the specified range; can be empty or omitted |
| `result`       | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       | `♥result`                                                   | Name of a variable where the command's result will be stored |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

In the script below, the robot opens a Google Sheets document, gets a value from the B16:B19 range and displays it in a dialog box:

```G1ANT
googlesheet.open 1gKFnrtZ-kzijNeIpYxln6PZS0z5btyHjoW1vZhCZ58c
googlesheet.getvalue B16:B19 result ♥values
dialog ♥values
googlesheet.close
```

