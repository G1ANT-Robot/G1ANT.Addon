# ui.settext

## Syntax

```G1ANT
ui.settext wpath ⟦wpath⟧ text ⟦text⟧
```

## Description

This command inserts text into a specified UI element of a desktop application window.

| Argument       | Type                                                         | Required | Default Value                                                | Description                                                  |
| -------------- | ------------------------------------------------------------ | -------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| `wpath`        | [wpath](G1ANT.Addon/G1ANT.Addon.UI/G1ANT.Addon.UI/Structures/WPathStructure.md) | yes      |                                                              | Desktop application window to be referred to                 |
| `text`         | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes      |                                                              | Text to be inserted into a specified UI element              |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                         | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                              | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                              | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                              | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                              | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

In the following script the robot opens Windows Explorer in a user’s Desktop folder, then enters “.txt” in the Explorer’s search box:

```G1ANT
program explorer arguments ♥environment⟦USERPROFILE⟧\Desktop
ui.settext ‴/ui[@name='Desktop']/ui[@id='40965']/descendant::ui[@id='SearchEditBox']‴ text .txt
```

If you get “*Access denied*” error message when using the `ui.settext` command, run G1ANT.Robot as administrator.

> **Note:** The names of window elements are system language dependent, so scripts with WPaths to these elements can be used only in the same language versions of Windows.
>
> To insert a WPath to an element, open a Windows Tree panel (select `Windows Tree` from `View` menu) expand the tree of an application window, navigate to a desired element and double-click it. Its WPath will be automatically inserted into the `ui.` command.
