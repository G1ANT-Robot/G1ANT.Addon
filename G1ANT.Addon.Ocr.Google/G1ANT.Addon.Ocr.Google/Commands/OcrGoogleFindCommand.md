# ocrgoogle.find

**Syntax:**

```G1ANT
ocrgoogle.find  search ‴‴
```

**Description:**

Command `ocrgoogle.find` allows to find the text on the current screen and return its position in [rectangle](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/rectangle.md)  format.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`search`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | required text to be found on the screen; only single words should be provided |
|`area`| [rectangle](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/rectangle.md) | no|  | additional parameter narrowing the search area to only certain part of the screen-example: ‴0//0//1920//1080‴ |
|`relative`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no| true | if false rectangle coordinates will be transformed to absolute, if true, a rectangle passed will crop from current active window |
|`result`| [rectangle](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/rectangle.md) | no | [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of a variable where the result rectangle will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutocr](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Ocr.Google.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Ocr.Google](https://github.com/G1ANT-Robot/G1ANT.Addon.Ocr.Google)

**Example 1:**

In order to use any ocrgoogle. command, it is necessary to be logged in to Google Cloud Platform. Please see "ocrgoogle.login":{TOPIC-LINK+ocrgoogle-login} command to see how to create an account and log in.
In this example we are opening G1ANT website, then logging in to Google Cloud Platform to be able to use `ocrgoogle.find` command. It searches for chosen word, in our case- 'Robotise' and saves it inside of a variable thanks to the **result** argument.

```G1ANT
chrome http://g1ant.com/
ocrgoogle.login jsoncredential ‴0b7239b2d48b60d4b5bc45c5297e57002f611e6x‴
ocrgoogle.find search ‴Robotise‴ result ♥foundtext
dialog ♥foundtext
```

