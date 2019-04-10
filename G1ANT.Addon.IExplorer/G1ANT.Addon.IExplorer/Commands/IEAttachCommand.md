# ie.attach

## Syntax

```G1ANT
ie.attach phrase ⟦text⟧ by ⟦text⟧
```

## Description

This command attaches G1ANT.Robot to an already running Internet Explorer instance and is required for other `ie.` commands to work properly if the [`ie.open`](IEOpenCommand.md) command was not used to open IE and attach the robot to the browser. This command also activates a tab found by a specified phrase.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`phrase`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Browser tab title or URL address |
|`by`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no | title | Determines where to search for a  phrase in a tab to activate it: `title` or `url` |
|`result`| [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no | `♥result` | Name of a variable where the command's result (an attached Internet Explorer instance ID) will be stored |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

This example attaches G1ANT.Robot to a previously opened Internet Explorer instance, picking it by the website name (Google) in its title:

```G1ANT
program iexplore arguments google.com
ie.attach google by title
```

