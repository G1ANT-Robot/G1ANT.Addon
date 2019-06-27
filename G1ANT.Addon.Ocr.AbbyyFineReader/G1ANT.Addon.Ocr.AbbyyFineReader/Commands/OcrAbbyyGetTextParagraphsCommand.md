# ocrabbyy.gettextparagraphs

------

## Syntax

```
ocrabbyy.gettextparagraphs documentid ⟦integer⟧
```

## Description

This command extracts text paragraphs from a specified file.

> **Note:** The OCR ABBYY addon is in the beta phase and was not tested with ABBYY FineReader Engine 12.

| Argument       | Type                                                         | Required | Default Value                                                | Description                                                  |
| :------------- | :----------------------------------------------------------- | :------- | :----------------------------------------------------------- | :----------------------------------------------------------- |
| `documentid`   | [integer](G1ANT.Language/G1ANT.Language/Structures/IntegerStructure.md) | no       |                                                              | ID of a processed document. If not specified, the last processed document is used |
| `result`       | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       | `♥result`                                                    | Name of a variable where the command's result (a list of recognized paragraphs) will be stored |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                         | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                              | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                              | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                              | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                              | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

In order to use the `ocrabbyy.gettextparagraphs` command, it’s necessary to process the file first with the [`ocrabbyy.processfile`](G1ANT.Addon/G1ANT.Addon.Ocr.AbbyyFineReader/G1ANT.Addon.Ocr.AbbyyFineReader/Commands/OcrAbbyyProcessFileCommand.md) command. In the example below, a sample file located on user’s Desktop is processed and then the robot assigns contents of each text paragraph to an element of a resulting list. Finally, this list is displayed in a dialog box:

```G1ANT
ocrabbyy.processfile ♥environment⟦USERPROFILE⟧\Desktop\document.jpg result ♥fileId
ocrabbyy.gettextparagraphs ♥fileId result ♥paragraph
dialog ♥paragraph
```

