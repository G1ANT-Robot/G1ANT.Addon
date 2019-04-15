# ie.setattribute

## Syntax

```G1ANT
ie.setattribute name ⟦text⟧ search ⟦text⟧ by ⟦text⟧ value ⟦text⟧ nowait ⟦bool⟧
```

## Description

Command `ie.setattribute` allows to set attribute's value of the specified element.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`name`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Name of an attribute |
|`search`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes | | Phrase to find an element by |
|`by`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no | id | Specifies an element selector: `id`, `name`, `text`, `title`, `class`, `selector`, `query`, `jquery` |
|`value`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no |  | Value to be set |
|`nowait`| [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no | false | If set to `true`, the robot will not wait until the action is completed |
|`result`| [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no | `♥result` | Name of a variable where the command's result will be stored |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

In this example the robot opens DuckDuckGo search in Internet Explorer, focuses on the browser window, waits 2 seconds and then searches for an element “*q*” (the search input box) by its name and sets its attribute “*value*” (the phrase to be searched for) to “*G1ANT*”. The word “G1ANT” will appear in the search input box as a result:

```G1ANT
ie.open duckduckgo.com
window ‴✱internet explorer✱‴
delay 2
ie.setattribute ‴value‴ search q by name value G1ANT
```

> **Note:** The element could also be searched by other selectors: `id`, `text`, `title`, `class`, `selector`, `query`, `jquery`. In order to search any element on a website using the `ie.setattribute` command, you need to use web browser developer tools (right-click an element and select `Inspect element` or `Inspect` from the resulting context menu).

> Before using this command, the [`ie.attach`](IEAttachCommand.md) or the [`ie.open`](IEOpenCommand.md) command has to be executed.
