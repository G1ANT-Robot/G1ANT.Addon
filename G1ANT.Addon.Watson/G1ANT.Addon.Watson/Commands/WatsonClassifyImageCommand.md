# watson.classifyimage

## Syntax

```G1ANT
watson.classifyimage imagepath ⟦text⟧ apikey ⟦text⟧ serveruri ⟦text⟧ threshold ⟦float⟧
```

## Description

This command classifies a specified image. Class scores range from 0 - 1, where a higher score indicates greater likelihood of the class being depicted in the image.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`imagepath`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Path to an image file to be classified |
|`apikey`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | API key needed to log in to the service |
|`serveruri`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |                                            | IBM server URI |
|`threshold`| [float](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/FloatStructure.md) | no | 0.5 | Floating point value (0-1 range) that specifies a minimum score a class must have to be displayed in the results |
| `result`       | [variable](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       | `♥result`                                                   | Name of a variable where the command's result will be stored |
| `if`           | [bool](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutwatson](https://manual.g1ant.com/link/G1ANT.Addon.Watson/G1ANT.Addon.Watson/Variables/TimeoutWatsonVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](https://manual.g1ant.com/link/G1ANT.Manual/appendices/common-arguments.md) page.

### Generating apikey

In order to generate the required `apikey` argument, log in to your [IBM Cloud account](https://cloud.ibm.com/login) (if you don’t have an account yet, [create a free account](https://cloud.ibm.com/registration)) and follow [these instructions](https://cloud.ibm.com/docs/resources?topic=resources-externalapp#externalapp).

## Example

This script will show Watson’s classification of a specified image (be sure to provide a real API key and an image filepath):

```G1ANT
♥apiKey = Enter your apikey here
♥serverUri = https://gateway.watsonplatform.net/visual-recognition/api/v3/classify?version=2018-03-19
watson.classifyimage C:\Test\image.jpg apikey ♥apiKey serveruri ♥serverUri
dialog ♥result
```


