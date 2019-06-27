# googlesheet.download

## Syntax

```G1ANT
googlesheet.download path ⟦text⟧ type ⟦text⟧
```

## Description

This command downloads the whole spreadsheet.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`path`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Destination on your computer where the file will be saved |
|`type`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no | xlsx | Type of the downloaded file (extension): `pdf` or `xlsx` |
| `result`       | [variable](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       | `♥result`                                                   | Name of a variable where the command's result will be stored |
| `if`           | [bool](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](https://manual.g1ant.com/link/G1ANT.Manual/appendices/common-arguments.md) page.

## Example

This simple script opens a Google Sheet document, then downloads it to an Excel file on the user’s Desktop and closes the sheet:

```G1ANT
googlesheet.open 1w5iopoKzgALxC1Qumtzvmc4VkXPq6kgkxieISibBpTs
googlesheet.download ♥environment⟦USERPROFILE⟧\Desktop\file.xlsx type xlsx
googlesheet.close
```


