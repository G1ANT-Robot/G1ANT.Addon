# wpath

This structure stores WPath information of UI elements and is used by the `ui.` family of commands, `waitfor.ui` included.

The WPath information can be obtained from G1ANT.Robot’s Windows Tree panel (select `Windows Tree` from `View` menu): expand the tree of a given application window, navigate to a desired element and double-click it. Its WPath will be automatically inserted into the command.

Note that the names of window elements are system language dependent, so scripts with WPaths to these elements can be used only in the same language versions of Windows.

## Example

In the script below, the robot opens Character Map utility, then uses the WPath of its “*Characters to copy*” input box to enter sample characters:

```G1ANT
program charmap
ui.settext ‴/ui[@name='Character Map']/ui[@id='104']‴ text G1ANT!
```

