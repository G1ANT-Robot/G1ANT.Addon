# ie.switch

## Syntax

```G1ANT
ie.switch id ⟦integer⟧
```

## Description

This command switches context to the already opened/attached Internet Explorer instance.
| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`id`| [integer](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/IntegerStructure.md) | yes |  | ID of the Internet Explorer instance to switch to |
| `if`           | [bool](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](https://manual.g1ant.com/link/G1ANT.Manual/appendices/common-arguments.md) page.

## Example

In this example the robot opens G1ANT website in Internet Explorer and assigns this instance’s ID to the `♥g1ant` variable. Then DuckDuckGo is opened in another IE instance and its ID is assigned to the `♥duck` variable. Using these variables, you can switch between these IE instances, so the robot switches to the first instance and gets its title, which is displayed in a dialog box:

```G1ANT
ie.open g1ant.com result ♥g1ant
ie.open duckduckgo.com result ♥duck
ie.switch ♥g1ant
ie.gettitle
dialog ♥result
```

> **Note:** Before using this command, the [`ie.attach`](https://manual.g1ant.com/link/G1ANT.Addon/G1ANT.Addon.IExplorer/G1ANT.Addon.IExplorer/Commands/IEAttachCommand.md) or the [`ie.open`](https://manual.g1ant.com/link/G1ANT.Addon/G1ANT.Addon.IExplorer/G1ANT.Addon.IExplorer/Commands/IEOpenCommand.md) command has to be executed.
