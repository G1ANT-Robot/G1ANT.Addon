# word.export

**Syntax:**

```G1ANT
word.export  path ‴‴
```

**Description:**

Command `word.export` exports document from currently active Word instance to specified file (in either .pdf or .xps format).

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`path`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | new path where your file will be saved |
|`type`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no | | type to export to (it can be either **pdf** or **xps**), if not specified type is inferred from file path extension|
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.MSOffice.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice](https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice)

**Example 1:**

This example exports currently active Word instance to specified path using `word.export` command.
First, open the word file using `word.open` command, then use `word.export` and in **path** argument, specify where on your computer you would like to have a new file saved. Add file extension '.pdf' or '.xps'.

```G1ANT
word.open path ‴C:\Public\doc1.docx‴
word.export path ‴C:\Public\doc2.pdf‴
```

You can see an opened Word file above.

Here we can see a newly created .pdf file after `word.export` command was executed.

**Example 2:**

In this example we are assigning some text to a variable ♥toInsert. Then using `word.open` command we are opening an instance of Word document and inserting text from the variable. In another step, thanks to using `word.save` command we are saving our Word document under 'test.docx' name in specified location. After that, `word.export` command lets us create a .pdf file from .docx file.

```G1ANT
♥toInsert = ‴Animi, id est laborum et dolorum fuga. Fugiat quo voluptas nulla pariatur? Architecto beatae vitae dicta sunt explicabo. Accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo. Do eiusmod tempor incididunt ut labore et dolore magna aliqua.‴
word.open
word.inserttext text ♥toInsert replacealltext true
word.save path ‴C:\Tests\test.docx‴
word.export path ‴C:\Tests\test.pdf‴ type ‴pdf‴
```

Please, be aware that in order to export and save files in specified locations on your computer, you need to have file and folder permissions.
