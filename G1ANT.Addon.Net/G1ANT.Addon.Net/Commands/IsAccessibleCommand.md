# is.accessible

**Syntax:**

```G1ANT
is.accessible  hostname ‴‴
```

**Description:**

Command `is.accessible` allows to check if host is accessible.
| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`hostname`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md)  | yes  | | name of host that we are trying to access |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Net.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Net](https://github.com/G1ANT-Robot/G1ANT.Addon.Net)

**Example 1**:

This example checks whether google.com is accessible using `is.accessible` command, then result("true") is displayed in `dialog` command.

```G1ANT
is.accessible hostname ‴www.google.com‴ result ♥result
dialog ♥result
```

**Example 2:**

```G1ANT
is.accessible hostname ‴www.google.com‴ result ♥access
dialog ♥access
```
