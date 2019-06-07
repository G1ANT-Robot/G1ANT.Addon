# ie.getattribute

## Syntax

```G1ANT
ie.getattribute name ⟦text⟧ search ⟦text⟧ by ⟦text⟧
```

## Description

This command gets the attribute value of a specified element.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`name`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Name of an attribute |
|`search`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes | | Phrase to find an element by |
|`by`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no | id | Specifies an element selector: `id`, `name`, `text`, `title`, `class`, `selector`, `query`, `jquery` |
|`nowait`| [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no | false | If set to `true`, the robot will not wait until the action is completed |
|`result`| [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no | `♥result` | Name of a variable where the command's result will be stored |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutie](G1ANT.Addon.IExplorer/G1ANT.Addon.IExplorer/Variables/TimeoutIEVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

This example gets and displays the values of  the `title` and the `role` attributes of an element found by a specified name on Google page (a search box in this case):

```G1ANT
ie.open google.com
ie.getattribute title search q by name
dialog ♥result
ie.getattribute role search q by name
dialog ♥result
```

> **Note:** The element could also be searched by other selectors: `id`, `text`, `title`, `class`, `selector`, `query`, `jquery`. In order to search any element on a website using the `ie.getattribute` command, you need to use web browser developer tools (right-click an element and select `Inspect element` or `Inspect` from the resulting context menu).
>
> Before using this command, the [`ie.attach`](IEAttachCommand.md) or the [`ie.open`](IEOpenCommand.md) command has to be executed.
