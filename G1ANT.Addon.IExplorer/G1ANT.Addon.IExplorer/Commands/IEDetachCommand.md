# ie.detach

## Syntax

```G1ANT
ie.detach
```

## Description

This command detaches the currently running Internet Explorer instance — attached or opened in G1ANT.Robot.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

In the example below two IE instances are opened and an ID of each one is assigned to a respective variable. The robot switches to the first instance, gets its title and displays it in a dialog box (“*Google*”). Then, this instance is detached from the robot, so when the script tries to switch to the Google window again, an error occurs:

```G1ANT
ie.open google.com result ♥id1
ie.open bing.com result ♥id2
ie.switch ♥id1
ie.gettitle
dialog ♥result
ie.detach
ie.switch ♥id1
```

> **Note:** Before using this command, the [`ie.attach`](IEAttachCommand.md) or the [`ie.open`](IEOpenCommand.md) command has to be executed.
