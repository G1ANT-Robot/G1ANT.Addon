# rest

**Syntax:**

```G1ANT
rest  method ‴‴
```

**Description:**

Command `rest` prepares a request to the desired URL with selected method. Multiple parameters can be passed and all of them will be attached as a request body.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`method`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | http method of rest request ( post / get / put / delete / patch ...)|
|`url`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md)  | yes|  | URL of API method |
|`headers`| [list](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/list.md)  | no |  |headers attached to rest request, separate headers using ❚ |
|`parameters`| [list](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/list.md)  | no |  | parameters attached to rest request, separate headers using ❚ |
|`result`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md)  | no |  [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  |name of variable which will return the data responded by API (usually json or xml) |
|`status`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md)  | no | |name of variable which will return http status responded by API  |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutrest](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Net.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Net](https://github.com/G1ANT-Robot/G1ANT.Addon.Net)

**Example 1:**

```G1ANT
rest method ‴get‴ url ‴https://httpbin.org/get‴ headers ‴type1:xml‴ parameters ‴par1:value‴
dialog ♥result
```

**Example 2:**

```G1ANT
rest method ‴post‴ url ‴https://httpbin.org/post‴ parameters ‴par1:value‴
dialog ♥result
```

**Example 3:**

```G1ANT
rest method ‴get‴ url ‴https://httpbin.org/get‴ result ♥result timeout 5000
dialog ♥result
```

