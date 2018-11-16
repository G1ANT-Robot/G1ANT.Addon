# ie.switch

**Syntax:**

```G1ANT
ie.switch  id ‴‴
```

**Description:**

Command `ie.switch` switches context to already opened/attached Internet Explorer instance. Required argument: id.
| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`id`| [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md)  | yes |  | ID number of Internet Explorer instance to switch to  |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutie](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.IExplorer.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer](https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer)

**Example 1:**

In order to use `ie.switch` command, first open IE windows using `ie.open` command, while opening give them the `result` argument. You will need to store the result in a variable to use it later for `ie.switch` command.

```G1ANT
ie.open url ‴g1ant.com‴ result ♥IeG1ant
ie.open url ‴duckduckgo.com‴ result ♥IeDuck
ie.switch ♥IeG1ant
ie.gettitle result ♥title
dialog ♥title
```

**Example 2:**

```G1ANT
ie.open result ♥result1
ie.open result ♥result2
ie.open result ♥result3
ie.open result ♥result4
ie.switch ♥result2
ie.switch ♥result3
ie.switch ♥result4
ie.switch ♥result1
ie.close
ie.switch ♥result2
ie.close
ie.switch ♥result3
ie.close
ie.switch ♥result4
ie.close
```
