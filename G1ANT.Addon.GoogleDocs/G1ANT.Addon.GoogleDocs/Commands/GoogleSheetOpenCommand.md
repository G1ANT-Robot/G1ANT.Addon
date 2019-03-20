# googlesheet.open

## Syntax

```G1ANT
googlesheet.open id ⟦text⟧ isshared ⟦bool⟧
```

## Description

This command opens a new Google Sheets instance.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`id`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Google Sheets document ID |
|`isshared`| [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no | true | Specifies whether a document |
| `result`       | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       | `♥result`                                                   | Name of a variable where the instance ID will be stored |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

To open a Google Sheets document, you need its ID. You can find it in the document’s URL address between two slashes after the `https://docs.google.com/spreadsheets/d/` prefix:

https://docs.google.com/spreadsheets/d/**Document_ID**/edit#gid=0

When the document is opened, the robot assigns its own ID to this Google Sheets instance and you can use it later to close this particular instance with the [`googlesheet.close`](GoogleSheetCloseCommand.md) command or switch to it with the [`googlesheet.switch`](GoogleSheetSwitchCommand.md) command.

```G1ANT
googlesheet.open 1gKFnrtZ-kzijNeIpYxln6PZS0z5btyHjoW1vZhCZ58c result ♥sheetId
googlesheet.close ♥sheetId
```
