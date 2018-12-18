# ie.setattribute

**Syntax:**

```G1ANT
ie.setattribute  name ‴‴ search ‴‴
```

**Description:**

Command `ie.setattribute` allows to set attribute's value of the specified element.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`name`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | name of attribute |
|`search`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | phrase to find element by |
|`value`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | value to set |
|`by`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | specifies an element selector, possible values are: 'name', 'text', 'title', 'class', 'id', 'selector', 'query', 'jquery'|
|`nowait`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | false | if true script will continue without waiting for webpage to respond to the event |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutie](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.IExplorer.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer](https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer)

**Example 1:**

This example sets value of the attribute's title of the element found by specified ID.

```G1ANT
ie.attach ‴G1ANT‴
ie.setattribute name ‴title‴ value ‴G1ANT‴ search ‴lst-ib‴ by id result ♥title
ie.detach
```

**Example 2:**

`by` argument will accept both value with ‴ ‴ or without.

```G1ANT
ie.open url ‴google.com‴
ie.getattribute name ‴role‴ search ‴lst-ib‴ by id result ♥title
dialog ♥title
ie.setattribute name ‴role‴ value ‴Octopus‴ search ‴lst-ib‴ by ‴id‴
ie.getattribute name ‴role‴ search ‴lst-ib‴ by id result ♥title
dialog ♥title
```
