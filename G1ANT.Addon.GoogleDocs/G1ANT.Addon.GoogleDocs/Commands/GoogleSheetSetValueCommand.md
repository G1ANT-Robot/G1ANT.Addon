# googlesheet.setvalue

## Syntax

```G1ANT
googlesheet.setvalue range ⟦text⟧ value ⟦text⟧ sheetname ⟦text⟧ numeric ⟦bool⟧
```

## Description

This command sets a value in an opened Google Sheets instance.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`range`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Cell or range address (e.g. `A6`) where new data will be entered |
|`value`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes | | New value to be entered into a specified cell or range |
|`sheetname`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no |  | Name of the sheet which contains the specified cell or range; can be empty or omitted |
|`numeric`| [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no | true | Determines if a new value should be entered as numeric or not |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

In the following script a Google Sheet document is opened and a value of *525* is entered into the cell *I2* as number:

```G1ANT
googlesheet.open 1gKFnrtZ-kzijNeIpYxln6PZS0z5btyHjoW1vZhCZ58c
googlesheet.setvalue range I2 value 525 numeric true
googlesheet.close
```

