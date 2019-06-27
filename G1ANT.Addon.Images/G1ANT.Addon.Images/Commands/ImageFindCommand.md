# image.find

## Syntax

```G1ANT
image.find image1 ⟦text⟧ image2 ⟦text⟧ screensearcharea ⟦rectangle⟧ relative ⟦bool⟧ threshold ⟦float⟧ centerresult ⟦bool⟧ offsetx ⟦integer⟧ offsety ⟦integer⟧
```

## Description

This command finds a specified image in another image (or in a part of the screen/entire screen) and returns the coordinates of the matching image. These coordinates point to the matching image’s top-left or the center (default) pixel, depending on the `centerresult` argument.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`image1`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Path to a file with an image be found |
|`image2`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no |  | Path to a image file in which `image1` will be searched; if not specified, `image1` will be searched on the screen |
|`screensearcharea`| [rectangle](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/RectangleStructure.md) | no | (equal to the current screen area) | Narrows the search area to a rectangle specified by coordinates in the `x0⫽y0⫽x1⫽y1` format, where `x0⫽y0` and `x1⫽y1` are the pixel coordinates in the top left corner and the bottom right corner of the rectangle, respectively |
|`relative`| [bool](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no | true| Specifies whether the search should be done relatively to the active window |
|`threshold`| [float](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/FloatStructure.md) | no | 0 | Tolerance threshold (0-1 range); the default 0 means it has to be a 100% match |
|`centerresult`| [bool](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no | true | If specified, the resulting point will be placed in the center of the matching area |
|`offsetx`| [integer](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/IntegerStructure.md) | no | 0 | Value that will be added to the result's X coordinate |
|`offsety`| [integer](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/IntegerStructure.md) | no | 0 | Value that will be added to the result's Y coordinate |
| `result`       | [variable](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       | `♥result`                                                   | Name of a variable where the command's result will be stored |
| `if`           | [bool](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutimagefind](G1ANT.Addon/G1ANT.Addon.Images/G1ANT.Addon.Images/Variables/TimeoutImageFindVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](https://manual.g1ant.com/link/G1ANT.Manual/appendices/common-arguments.md) page.

## Example

In the following script the robot downloads a sample image file to the user’s Desktop, then opens it with a default program, checks whether this image appeared on the screen and displays the coordinates of its center pixel in a dialog box:

```G1ANT
♥image = ♥environment⟦USERPROFILE⟧\Desktop\image.png
file.download https://jeremykun.files.wordpress.com/2012/01/img49.png filename ♥image
program ♥image
image.find ♥image result ♥point relative false
dialog ♥point
```

