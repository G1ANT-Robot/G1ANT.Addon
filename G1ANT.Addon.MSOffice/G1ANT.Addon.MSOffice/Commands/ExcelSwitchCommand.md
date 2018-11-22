# excel.switch

**Syntax:**

```G1ANT
excel.switch  id ‴‴ 
```

**Description:**

Command `excel.switch` allows to switch from one active Excel file to another (only those which have been opened by G1ANT.Language).

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`id`| [integer](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/integer.md) | yes |  |  ID number of Excel instance that will be activated |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutcommand](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md)  | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.MSOffice.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice](https://github.com/G1ANT-Robot/G1ANT.Addon.MSOffice)

**Example 1:**

```G1ANT
excel.switch id 1
```

In this example the Excel window no. 1 is activated.

**Example 2:**

In order to use ID argument for `excel.switch` command, you first need to set an ID while using `excel.open` command.

```G1ANT
excel.open inbackground true result ♥excel1
excel.open path ‴C:\Users\diana\Desktop\test.xlsx‴ result ♥excel2
excel.switch id ♥excel1
```

**Example 3:**

```G1ANT
excel.open result ♥res1
excel.setvalue value ‴excel 1‴ row 1 colindex 1
excel.open result ♥res2
excel.open result ♥res3
excel.open result ♥res4
dialog ‴♥res1 ♥res2 ♥res3 ♥res4‴
excel.switch id 0
excel.setvalue value ‴excel 1‴ row 2 colindex 1
```

Let's look at the script above; we are opening Excel instance and saving it in `result ♥res1` to be able to refer to it later. Using `excel.setvalue` command, we can insert 'excel 1' text inside of first row in the first column. Next, we are opening another Excel instances. While using `dialog` command, we can see that the results stored in variables are numbers of opened instances, every time Excel activates a new instance, the number of it is incremented by 1. It starts with number 0. That is why while using `dialog` command, id '0' was assigned to ♥res1.

While switching to the Excel instance with **id 0**, G1ANT.Robot opens the first Excel instance from our script.

