# ie.fireevent

**Syntax:**

```G1ANT
ie.fireevent  eventname ‴‴ 
```

**Description:**

Command `ie.fireevent` allows to fire specified event on specified element.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`eventname`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | name of event to fire- can be any HTML DOM event |
|`parameters`| [list](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/list.md) | no |  | parameters to be passed to the event handler |
|`search`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes | | phrase to find element by |
|`by`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no | id | specifies an element selector, possible values are: 'name', 'text', 'title', 'class', 'id', 'selector', 'query', 'jquery'|
|`nowait`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | false | if true script will continue without waiting for webpage to respond to the event |
|`timeout`| [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md) | no | [♥timeoutie](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies maximum number of milliseconds to wait for command to execute |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.IExplorer.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer](https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer)

**Example 1:**

In this example ‴google.com‴ is opened. Then G1ANT.Robot waits for 5 seconds and fires a click event using `ie.fireevent` command and giving `eventname` argument the value of 'onclick'. 'Onclick' will be executed on the element which is found with `search` argument. In that case it is Gmail icon. To search for the icon in HTML, use IE Developer tools.

```G1ANT
ie.open url ‴google.com‴
delay seconds 5
ie.fireevent eventname ‴onclick‴ search ‴[href='https://mail.google.com/mail/?tab=wm']‴ by query timeout 15000
ie.gettitle result ♥title
dialog ♥title
```

