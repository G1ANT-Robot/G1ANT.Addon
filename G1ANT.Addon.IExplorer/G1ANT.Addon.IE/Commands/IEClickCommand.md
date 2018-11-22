# ie.click

**Syntax:**

```G1ANT
ie.click  search ‴‴
```

**Description:**

Command `ie.click` clicks an element on an active webpage.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`search`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes | | phrase to find element by |
|`by`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no | id | specifies an element selector, possible values are:  'id', 'name', 'text', 'title', 'class', 'selector', 'query', 'jquery' |
|`nowait`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | false | if true, the script will continue without waiting for webpage to respond to click event |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutie](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.IExplorer.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer](https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer)

**Example 1:**

This example attaches to Internet Explorer and clicks button with text "How".

```G1ANT
ie.open url ‴g1ant.com‴
ie.attach ‴G1ANT‴
ie.click ‴How‴ by ‴text‴
ie.detach
```

**Example 2:**

In this example, `ie.click` command will find the search button by its id. In order to check what the name of id is, you need to use IE Developer Tools.

```G1ANT
ie.open url ‴duckduckgo.com‴
window ‴✱internet explo✱‴
ie.click search ‴search_button_homepage‴ by ‴id‴
keyboard text ‴anglerfish‴
ie.click search ‴search_button_homepage‴ by ‴id‴
```

