# googlesheet.close

## Syntax

```G1ANT
googlesheet.close id ⟦integer⟧
```

## Description

This command closes a Google Sheets instance.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`id`| [integer](G1ANT.Language/G1ANT.Language/Structures/IntegerStructure.md) | no |  | ID of a spreadsheet to be closed. The ID can be stored in a variable when the [`googlesheet.open`](GoogleSheetOpenCommand.md) command is used. If no ID is specified, a recently used Google Sheets instance is closed |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

In this example two Google Sheets instances are opened (be sure to provide their real document IDs) and then the first one is closed. If no ID was specified in the `googlesheet.close` command, the second instance would be closed since it was the last used.

```G1ANT
googlesheet.open 1w5iopoKzgALxC1Qumtzvmc4VkXPq6kgkxieISibBpTs result ♥sheetId1
googlesheet.open 1iCL_st5tCA54jCiLJ7pYScw-3P79A96pxgzeVZmv_aM result ♥sheetId2
googlesheet.close ♥sheetId1
```



