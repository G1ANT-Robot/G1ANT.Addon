# ping

## Syntax

```G1ANT
ping ip ⟦text⟧ repeats ⟦integer⟧
```

## Description

This command pings a specified IP address and returns an approximate round-trip time in milliseconds.
| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`ip`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no  | 8.8.8.8 | IP address or a host name of a pinged server |
|`repeats`| [integer](G1ANT.Language/G1ANT.Language/Structures/IntegerStructure.md) | no | 1 | Allows to ping multiple times; the command returns a rounded average of all pings |
|`result`| [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no | ♥result  | Name of a variable where approximate round trip time in milliseconds will be stored |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutconnect](G1ANT.Addon/G1ANT.Addon.Net/G1ANT.Addon.Net/Variables/TimeoutConnectVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

This example pings `google.com` server and the approximate round trip time is displayed in milliseconds in a dialog box:

```G1ANT
ping google.com
dialog ♥result
```
