# ie.refresh

**Syntax:**

```G1ANT
ie.refresh 
```

**Description:**

Command `ie.open` allows to refresh current Internet Explorer instance.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutie](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.IExplorer.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer](https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer)

**Example 1:**

This example opens Internet Explorer and loads g1ant.com webpage. After loading it refreshes the page.

```G1ANT
ie.open ‴g1ant.com‴
ie.refresh
```

**Example 2:**

```G1ANT
♥variableF = true
ie.open url ‴g1ant.com‴ nowait false result ♥IeG1ant
ie.refresh if ♥variableF
```
