# ocrgoogle.login

**Syntax:**

```G1ANT
ocrgoogle.login  jsoncredential ‴‴
```

**Description:**

Command `ocrgoogle.login` allows to log in to the Google text recognition service.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`jsoncredential`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | Json credential obtained from Google text recognition service |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutocr](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Ocr.Google.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Ocr.Google](https://github.com/G1ANT-Robot/G1ANT.Addon.Ocr.Google)

**Example 1:**

It is crutial that you use `ocrgoogle.login` command before using any other ocrgoogle. command. While using `ocrgoogle.login` we need to specify the argument **jsoncredential**.
In order to be able to specify a value for **jsoncredential**, it is necessary to create an account on Google Cloud Platform.
Argument **jsoncredential** takes a special number as value. This number is generated after you create an account on https://cloud.google.com/vision/.
Once you obtain it, just use it as the value for **jsoncredential** argument and you will be able to use other useful ocrgoogle. commands.

In this example G1ANT.Robot is opening 'duckduckgo.com' and finding- using `ocrgoogle.findtext` 'Duckduckgo' text on the web page. 

```G1ANT
chrome url ‴duckduckgo.com‴
ocrgoogle.login jsoncredential ‴0b7239b2d48b60d4b5bc45c5297e57002f611e6x‴
ocrgoogle.find ‴Duckduckgo‴ result ♥foundtext
dialog ♥foundtext
```

