# ie.activatetab

**Syntax:**

```G1ANT
ie.activatetab  phrase ‴‴
```

**Description:**

Command `ie.activatetab` allows to activate Internet Explorer tab for further use by ie commands. Before using this command, "ie.attach":{TOPIC-LINK+command-ie-attach} command has to be invoked.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`phrase`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | browser tab title or url |
|`by`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no | title | 'title' or 'url', determines what to look for in a tab to activate |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutie](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.IExplorer.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer](https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer)

**Example 1:**

This example attaches Internet Explorer and activates tab with G1ANT webpage.

```G1ANT
ie.attach ‴Google‴
ie.activatetab ‴g1ant‴ by ‴url‴
ie.detach
```

**Example 2:**

```G1ANT
ie.attach phrase ‴amazon‴ by ‴title‴
ie.gettitle result ♥title
dialog ♥title
ie.activatetab phrase ‴bing‴ by ‴title‴
```

