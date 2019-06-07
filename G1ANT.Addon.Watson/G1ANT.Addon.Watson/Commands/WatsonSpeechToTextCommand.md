# watson.speechtotext

## Syntax

```G1ANT
watson.speechtotext path ⟦text⟧ apikey ⟦text⟧ serveruri ⟦text⟧ language ⟦text⟧
```

## Description

This command transcripts speech from an audio file.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`path`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Path to a file with recorded speech |
|`apikey`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | API key needed to log in to the service |
|`serveruri`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |                                            | IBM server URI |
|`language`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no | en-US | Language of speech |
| `result`       | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       | `♥result`                                                   | Name of a variable where the command's result will be stored |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutwatson](G1ANT.Addon.Watson/G1ANT.Addon.Watson/Variables/TimeoutWatsonVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

### Generating apikey

In order to generate the required `apikey` argument, log in to your [IBM Cloud account](https://cloud.ibm.com/login) (if you don’t have an account yet, [create a free account](https://cloud.ibm.com/registration)) and follow [these instructions](https://cloud.ibm.com/docs/resources?topic=resources-externalapp#externalapp).

## Example

This script will show a Watson’s transcription of a specified audio file (be sure to provide a real API key and an audio filepath):

```G1ANT
♥apiKey = Enter your apikey here
♥serverUri = https://gateway-lon.watsonplatform.net/speech-to-text/api
watson.speechtotext C:\Test\speech.mp3 apikey ♥apiKey serveruri ♥serverUri
dialog ♥result
```
