# googlesheet.switch

## Syntax

```G1ANT
googlesheet.switch id ⟦integer⟧ 
```

## Description

This command switches between opened Google Sheets instances.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`id`| [integer](G1ANT.Language/G1ANT.Language/Structures/IntegerStructure.md) | yes |  | title of Google Sheets instance that will be activated |
| `result`       | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       | `♥result`                                                   | Name of a variable where the command's result will be stored (`true` if it succeeded, `false` if it did not) |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

The example below opens a Google Sheets document, then retrieves and displays its title. The same actions are executed with another Google Sheets document. Then, the robot switches back to the first document, displays its title and closes the second document. It’s possible thanks to instance IDs stored by the robot in two `♥sheetId` variables:

```G1ANT
googlesheet.open 1gKFnrtZ-kzijNeIpYxln6PZS0z5btyHjoW1vZhCZ58c result ♥sheetId1
googlesheet.gettitle ♥title
dialog ♥title
googlesheet.open 1iCL_st5tCA54jCiLJ7pYScw-3P79A96pxgzeVZmv_aM result ♥sheetId2
googlesheet.gettitle ♥title
dialog ♥title
googlesheet.switch id ♥sheetId1
googlesheet.gettitle result ♥title
dialog ♥title
googlesheet.close id ♥sheetId2
```


