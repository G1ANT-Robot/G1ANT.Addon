# ie.fireevent

## Syntax

```G1ANT
ie.fireevent eventname ⟦text⟧ parameters ⟦list⟧ search ⟦text⟧ by ⟦text⟧ nowait ⟦bool⟧
```

## Description

This command fires a specified event on a specified element.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`eventname`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Name of an event to fire (can be any HTML DOM event) |
|`parameters`| [list](G1ANT.Language/G1ANT.Language/Structures/ListStructure.md) | no |  | Parameters to be passed to the event handler |
|`search`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes | | Phrase to find an element by |
|`by`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no | id | Specifies an element selector: `id`, `name`, `text`, `title`, `class`, `selector`, `query`, `jquery` |
|`nowait`| [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no | false | If set to `true`, the script will continue without waiting for a webpage to respond to the event |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutie](G1ANT.Addon.IExplorer/G1ANT.Addon.IExplorer/Variables/TimeoutIEVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

The following script opens Google in Internet Explorer. Then, the robot waits 5 seconds and fires a click event (`onclick` as a value for the `eventname` argument) on a specified element found by a query in the page source code. In this particular case it is a Gmail link, so a Gmail start page opens in the browser:

```G1ANT
ie.open google.com
delay 5
ie.fireevent onclick search [href='https://mail.google.com/mail/?tab=wm'] by query timeout 15000
ie.gettitle ♥title
dialog ♥title
```

> **Note:** The element could also be searched by other selectors: `id`, `name`, `text`, `title`, `class`, `selector`, `jquery`. In order to search any element on a website using the `ie.fireevent` command, you need to use web browser developer tools (right-click an element and select `Inspect element` or `Inspect` from the resulting context menu).
>
> Before using this command, the [`ie.attach`](IEAttachCommand.md) or the [`ie.open`](IEOpenCommand.md) command has to be executed.
