# ie.click

## Syntax

```G1ANT
ie.click search ⟦text⟧ by ⟦text⟧ nowait ⟦bool⟧
```

## Description

This command clicks an element on an active webpage.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`search`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes | | Phrase to find an element by |
|`by`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no | id | Specifies an element selector: `id`, `name`, `text`, `title`, `class`, `selector`, `query`, `jquery` |
|`nowait`| [bool](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no | false | If set to `true`, the script will continue without waiting for a webpage to respond to a click event |
| `if`           | [bool](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](https://manual.g1ant.com/link/G1ANT.Manual/appendices/common-arguments.md) page.

## Example

This example opens Google in Internet Explorer and clicks Google account login button using its ID:

```G1ANT
ie.open google.com
window ‴✱internet explorer✱‴
delay 2
ie.click gb_70 by id
```

> **Note:** The element could also be searched by other selectors: `name`, `text`, `title`, `class`, `selector`, `query`, `jquery`. In order to search any element on a website using the `ie.click` command, you need to use web browser developer tools (right-click an element and select `Inspect element` or `Inspect` from the resulting context menu).
>
> Before using this command, the [`ie.attach`](https://manual.g1ant.com/link/G1ANT.Addon/G1ANT.Addon.IExplorer/G1ANT.Addon.IExplorer/Commands/IEAttachCommand.md) or the [`ie.open`](https://manual.g1ant.com/link/G1ANT.Addon/G1ANT.Addon.IExplorer/G1ANT.Addon.IExplorer/Commands/IEOpenCommand.md) command has to be executed.
