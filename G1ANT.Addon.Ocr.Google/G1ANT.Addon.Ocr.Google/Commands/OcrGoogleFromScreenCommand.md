# ocrgoogle.fromscreen

**Syntax:**

```G1ANT
ocrgoogle.fromscreen  area ‴‴
```

**Description:**

Command `ocrgoogle.fromscreen` allows to capture part of the screen and recognise text from it. It uses internet connection and external data processing.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`area`| [rectangle](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/rectangle.md) | yes |  | specifies screen area to be captured in format x0//y0//x1//y1 (x0,y0 - coordinates of a top left corner; x1,y1 - coordinates of a right bottom corner of the area) |
|`relative`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | if 'true', coordinates are to be specified relatively to an active window |
|`result` | "variable":{TOPIC-LINK+boolean}| no | [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | stores the result of the command in a variable |
|`timeout`| [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md) | no | [♥timeout](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies amount of time (in miliseconds) for G1ANT.Robot to wait for the command to be executed |
|`languages`| [list](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/list.md) | no | en,fr | list of languages (`,` separated) that you want to use to recognize text on the screen, could also be one-element list, like “en”   |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Ocr.Google.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Ocr.Google](https://github.com/G1ANT-Robot/G1ANT.Addon.Ocr.Google)

**Example 1:**

`ocrgoogle.fromscreen` command reads everything from specified area. In order to use any ocrgoogle. command, it is necessary to be logged in to Google Cloud Platform. Please see "ocrgoogle.login":{TOPIC-LINK+ocrgoogle-login} command to see how to create an account and log in.

```G1ANT
ocrgoogle.login jsoncredential ‴0b7239b2d48b60d4b5bc45c5297e57002f611e6x‴
ocrgoogle.fromscreen area ‴530⫽446⫽1628⫽888‴ relative false result ♥fromscreen
dialog ♥fromscreen
```

