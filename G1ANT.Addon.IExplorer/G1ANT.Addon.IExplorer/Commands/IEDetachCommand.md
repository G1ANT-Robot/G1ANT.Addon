# ie.detach

**Syntax:**

```G1ANT
ie.detach
```

**Description:**

Command ie.detach allows to detach currently running Internet Explorer attached or opened in G1ANT.Robot.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`timeout`| [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md) | no | [♥timeoutie](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies amount of time (in miliseconds) for G1ANT.Robot to wait for the command to be executed |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.IExplorer.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer](https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer)

**Example 1:**

In this example two IE windows are opened with a result assigned to each of them in order to use it in `ie.switch` command. `ie.gettitle` gets the title of currently active window (‴google.com‴) as we switched to it before. `ie.detach` detaches ‴google.com‴. When we try to switch back to ‴google.com‴, an error will occur as we detached it before.

```G1ANT
ie.open url ‴google.com‴ result result1
ie.open url ‴www.bing.com‴ result result2
ie.switch ♥result1
ie.gettitle result title1
dialog ♥title1
ie.detach
ie.switch ♥result1
```
