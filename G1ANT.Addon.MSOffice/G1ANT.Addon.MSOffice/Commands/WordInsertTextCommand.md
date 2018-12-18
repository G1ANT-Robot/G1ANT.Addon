# word.inserttext

**Syntax:**

```G1ANT
word.inserttext  text ‴‴
```

**Description:**

Command `word.inserttext` allows to insert text inside of a Word document.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`text`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md)  | yes |  | text to be inserted |
|`replacealltext`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | false | defines whether all text should be replaced  |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.MSOffice.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice](https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice)

**Example 1:**

In this example we are assigning text to a variable and then inserting it to the file using `text.insert` command.

```G1ANT
♥toInsert = ‴I hate yogurt. It's just stuff with bits in. All I've got to do is pass as an ordinary human being. Simple. What could possibly go wrong? Saving the world with meals on wheels. I'm the Doctor, I'm worse than everyone's aunt. **catches himself** And that is not how I'm introducing myself.‴
word.open path ‴C:\Users\diana\Desktop\Cokolwiek.docx‴ result ♥wordHandleTest
word.inserttext text ♥toInsert replacealltext true
```

**Example 2:**

The text that we insert does not need to be assigned to a variable, we can simply use it as a string value for `text` argument.

```G1ANT
word.open
word.inserttext text ‴I am blah today...‴ replacealltext true
```
