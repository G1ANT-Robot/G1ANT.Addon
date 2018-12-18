# ie.attach

**Syntax:**

```G1ANT
ie.attach  phrase ‴‴
```

**Description:**

Command `ie.attach` allows to attach G1ANT.Robot to running Internet Explorer instance. This command invocation is required for other ie commands to work properly if you haven't used ie.open command before (which opens IE and attaches to it). This command also activates tab with a specified phrase.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`phrase`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | browser tab title or URL |
|`by`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no | title | title 'OR' url, determines what to look for in Internet Explorer to activate. |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of variable where [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md)  indicating success of attaching operation will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutie](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.IExplorer.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer](https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer)

**Example 1:**

This example attaches G1ANT.Robot to Internet Explorer.

```G1ANT
ie.attach phrase ‴Google‴
ie.detach
```

**Example 2:**

In this example G1ANT.Robot will stop running the script at line 4, bacause an error will occur. It cannot run `ie.gettitle` command as `ie.detach` command detached the instance before.

```G1ANT
ie.open url ‴google.com‴ result ♥IeHandle
ie.gettitle result ♥title1
ie.detach
ie.gettitle result ♥title2 errormessage ‴No internet explorer instance attached‴
ie.attach phrase ‴google‴ by ‴title‴
ie.seturl url ‴duckduckgo.com‴
ie.gettitle result ♥title2
dialog ♥title2
```

