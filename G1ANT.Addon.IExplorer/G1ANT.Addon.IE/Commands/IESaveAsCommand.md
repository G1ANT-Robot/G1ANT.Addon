# ie.saveas

**Syntax:**

```G1ANT
ie.saveas  path ‴‴
```

**Description:**

Command `ie.saveas` allows to automatically save file to specified directory once the pop-up box has appeared.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`path`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes | | specifies file's save path and save name |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutie](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.IExplorer.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer](https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer)

**Example 1:**

```G1ANT
ie.saveas path ‴C:\Download\file1‴
```

**Example 2:**

In this example G1ANT.Robot will open the specified IE instance. `delay` command is crutial while using `ie.saveas` command, because sometimes the browser does not load quickly enough before G1ANT.Robot executes the  `ie.saveas` command, which may cause an error occur.

```G1ANT
ie.open url ‴http://www.opera.com/pl/computer/thanks?ni=stable&amp;os=windows‴ nowait false
autodetachonclose false result ♥IeG1ant
delay 2
ie.saveas path ‴C:\Users\diana\Desktop\opera.exe‴
```
