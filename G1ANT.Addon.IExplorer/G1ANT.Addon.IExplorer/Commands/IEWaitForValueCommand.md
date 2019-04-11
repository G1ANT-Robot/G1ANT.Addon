# ie.waitforvalue

## Syntax

```G1ANT
ie.waitforvalue script ⟦text⟧ expectedvalue ⟦text⟧
```

## Description

This command keeps on executing a specified javascript until an expected value is returned.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`script`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes|  | Script to be evaluated in a browser |
|`expectedvalue`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Value that the script should return |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

In this example the robot opens G1ANT website in Internet Explorer, focuses on the browser window, then waits for the script  `document.getElementsByClassName("footer_stuff").length` to get a value different than null. Since the element of a class name *footer_stuff* is the last one on the page (it’s located at the bottom end), the robot in fact waits for the whole page to load. When it happens, a dialog box appears and the browser is closed:

```G1ANT
ie.open g1ant.com
window ‴✱internet explorer✱‴
ie.waitforvalue script document.getElementsByClassName("footer_stuff").length expectedvalue 1
dialog ‴Page loaded!‴
ie.close
```

> **Note:** Before using this command, the [`ie.attach`](IEAttachCommand.md) or the [`ie.open`](IEOpenCommand.md) command has to be executed.

