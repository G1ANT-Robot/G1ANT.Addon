# googlesheet.findall

## Syntax

```G1ANT
googlesheet.findall value ⟦text⟧ sheetname ⟦text⟧
```

## Description

This command finds all cells with a specified value.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`value`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Value to be searched for |
|`sheetname`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no |  | Sheet name where the search is to be performed; can be empty or omitted |
| `result`       | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       | `♥result`                                                   | Name of a variable where the command's result will be stored |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

This script searches Google Sheets document for a certain value, stores the cell addresses in the `♥timePlace` variable and then displays them in a dialog box:

```G1ANT
googlesheet.open 1gKFnrtZ-kzijNeIpYxln6PZS0z5btyHjoW1vZhCZ58c
googlesheet.findall value ‴8.00‴ sheetname ‴Time sheet‴ result ♥timePlace
dialog ♥timePlace
googlesheet.close
```

> **Note:** When you want to find numeric values in a Googlesheet document, you need to know how they are displayed in a particular document and enter the number for the `value` argument exactly as it’s presented in the spreadsheet. In the example above searching for “8” will not return any matches, since all numbers are formatted to show two decimal digits, even for integers. This is why the searched value is “8.00”. Likewise, floating point numbers such as 1.23456 have to be entered in a way they appear in a document: **1.235**, when they are displayed up to three decimal digits (with rounding); **1.23**, when two decimal digits are displayed, or **1**, when no decimal digits are displayed.
>
> Be also sure to embrace the searched number with ` ‴` character.

