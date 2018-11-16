# ocrtesseract.find

**Syntax:**

```G1ANT
ocrtesseract.find  search ‴‴
```

**Description:**

Command `ocrtesseract.find` allows to find the text on the active screen and it returns its position as a [rectangle](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/rectangle.md)  format. If text is not found, the result will be Rectangle(-1,-1,-2,-2). Please note that using this command results in unpacking the necessary data to directory My Documents/G1ANT.Robot.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`search`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | required text to be found on the screen- only single words should be provided |
|`area`| [rectangle](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/rectangle.md) | no |  | current screen resolution (i.e. 0,0,1920,1080) or additional parameter to find text only in certain part of screen |
|`relative`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | if false rectangle coordinates will be transformed to absolute, if true, a rectangle passed will crop from current active window |
|`result`| [rectangle](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/rectangle.md) | no |  [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of variable where result rectangle will be stored |
|`language`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no | eng | the language which should be considered trying to recognise text |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutocr](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Ocr.Tesseract.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Ocr.Tesseract](https://github.com/G1ANT-Robot/G1ANT.Addon.Ocr.Tesseract)

**Example 1:**

In this example we are opening G1ANT website firstly using `ie.open` command. Then we would like to search for word 'Robotise' on the web page. In order to do that, we can use `ocrtesseract.find`- this command searches for certain text that we should type as value for **search** argument. **area** argument enables us to choose either screen resolution or specified area where G1ANT.Robot should look for text.

```G1ANT
ie.open url ‴g1ant.com‴
ocrtesseract.find search ‴Robotise‴ area ‴791⫽340⫽1663⫽672‴ relative false result ♥ocrof
timeout 4000
dialog ♥ocrof
```

