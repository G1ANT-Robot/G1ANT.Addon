# ui.setroot

## Syntax

```G1ANT
ui.setroot wpath ⟦wpath⟧
```

## Description

This command sets a root window, to which other UI elements will refer to by WPath.

| Argument       | Type                                                         | Required | Default Value                                                | Description                                                  |
| -------------- | ------------------------------------------------------------ | -------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| `wpath`        | [wpath](G1ANT.Addon/G1ANT.Addon.UI/G1ANT.Addon.UI/Structures/WPathStructure.md) | yes      |                                                              | Desktop application window to be referred to                 |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                         | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                              | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                              | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                              | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                              | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

In the following script the robot opens Notepad, sets its window as a root element, clicks File menu, waits 2 seconds and closes Notepad by clicking its Close button.

As you can see, the WPaths in both `ui.click` commands don’t use the Notepad window title path, since it was already set and all UI elements specified in the `ui.` commands will refer to this window:

```G1ANT
program notepad
ui.setroot wpath ‴/ui[@name='Untitled - Notepad']‴
ui.click ‴/ui[@id='MenuBar']/ui[@name='File']‴
delay 2
ui.click wpath ‴/descendant::ui[@name='Close']‴
```

> **Note:** The names of window elements are system language dependent, so scripts with WPaths to these elements can be used only in the same language versions of Windows.
>
> To insert a WPath to an element, open a Windows Tree panel (select `Windows Tree` from `View` menu) expand the tree of an application window, navigate to a desired element and double-click it. Its WPath will be automatically inserted into the `ui.` command.
