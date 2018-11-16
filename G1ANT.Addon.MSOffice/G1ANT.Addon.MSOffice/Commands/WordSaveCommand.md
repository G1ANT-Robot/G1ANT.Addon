# word.save

**Syntax:**

```G1ANT
word.save  path ‴‴
```

**Description:**

Command `word.save` saves currently active Word document.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`path`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no | ‴C:\%username%\Documents‴ |specifies Word document save path|
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.MSOffice.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice](https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice)

**Example 1:**

This example saves currently active Word document to ‴C:\Documents\myfile\doc1.docx‴ using `word.save` command.

```G1ANT
word.save path ‴C:\Documents\myfile\doc1.docx‴
```

**Example 2:**

This example saves currently active Word document to ‴C:\Documents\doc1.docx‴ (directory is not specified, therefore a default one is chosen)

```G1ANT
word.save path ‴doc3.docx‴
```

**Example 3:**

This example shows how to insert text into a blank word document and save it choosing a path (the user has to have access to the catalogue of the chosen path).

```G1ANT
♥toInsert = ‴Animi, id est laborum et dolorum fuga. Fugiat quo voluptas nulla pariatur? Architecto beatae vitae dicta sunt explicabo. Accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo. Do eiusmod tempor incididunt ut labore et dolore magna aliqua.‴
word.open
word.inserttext text ♥toInsert replacealltext true
word.save path ‴C:\Tests\test.docx‴
```
