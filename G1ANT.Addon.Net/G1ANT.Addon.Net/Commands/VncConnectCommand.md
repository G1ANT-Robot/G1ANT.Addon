# vnc.connect

## Syntax

```G1ANT
vnc.connect host ⟦text⟧ port ⟦text⟧ password ⟦text⟧
```

## Description

This command connects to a remote machine with a running VNC server, using a remote desktop connection.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`host`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |   | IP or URL address of the remote machine |
|`port`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes|  | Port used to connect to the remote machine |
|`password`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes|  | Password used to connect to the remote machine               |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
|`timeout`| [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no | [♥timeoutremotedesktop](G1ANT.Addon/G1ANT.Addon.Net/G1ANT.Addon.Net/Variables/TimeoutRemoteDesktopVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

This script connects to a remote desktop using sample server data:

```G1ANT
vnc.connect host 10.0.0.1 port 5901 password vncpassword
```
