# ie.saveas

### Syntax

```G1ANT
ie.saveas path 
```

## Description

This command automatically saves file to a specified directory once the pop-up box has appeared.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`path`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes|  | Specifies the full file path |
|`result`| [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no | `♥result` | Name of a variable where the script result will be stored |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

This script opens a foobar2000 music player download page in Internet Explorer, focuses on the browser window, waits two seconds and then fills up the file details in *Save As* dialog box, saving the file under *foo.exe* name on the user’s Desktop:

```G1ANT
ie.open https://www.foobar2000.org/getfile/a1f3bb263a5bdba336264bbe58755a4b/foobar2000_v1.4.3.exe
window ‴✱internet explorer✱‴
delay 2
ie.saveas ♥environment⟦USERPROFILE⟧\Desktop\foo.exe
```

> **Note:** Before using this command, the [`ie.attach`](IEAttachCommand.md) or the [`ie.open`](IEOpenCommand.md) command has to be executed.

