# ping

**Syntax:**

```G1ANT
ping
```

**Description:**

Command `ping` allows to ping specified IP and receives approximate round-trip time in milliseconds.
| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`ip`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md)  | no  | 8.8.8.8 | IP address or hostname of pinging server |
|`repeats`| [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md)  | no |  | allows to ping multiple times, command returns rounded value of all pings. Default = 1 |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md)  | no | [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of variable where approximate round trip time in milliseconds will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Net.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Net](https://github.com/G1ANT-Robot/G1ANT.Addon.Net)

**Example 1**:

This example pings google.com using `ping` command, then approximate round trip time is displayed in milliseconds using `dialog` command.

```G1ANT
ping  ‴google.com‴
dialog ♥result
```

**Example 2:**

```G1ANT
ping ip ‴213.180.141.140‴ timeout 1000 result ♥pingResult
dialog ♥pingResult
```
