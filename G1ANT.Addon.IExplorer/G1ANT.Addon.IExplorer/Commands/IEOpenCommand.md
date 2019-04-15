# ie.open

## Syntax

```G1ANT
ie.open url ⟦text⟧ nowait ⟦bool⟧ autodetachonclose ⟦bool⟧
```

## Description

This command opens a new instance of Internet Explorer and navigates to a specified URL, if provided.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`url`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no |  | URL address to navigate to |
|`nowait`| [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no | false | If set to `true`, the command will not wait until the page is loaded |
|`autodetachonclose`| [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no | true | If set to `false`, the opened Internet Explorer instance will not be detached automatically when the script ends |
|`result`| [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no | `♥result` | Name of a variable where the command's result (an attached Internet Explorer instance ID) will be stored |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutie](G1ANT.Addon.IExplorer/G1ANT.Addon.IExplorer/Variables/TimeoutIEVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

This example opens G1ANT website in Internet Explorer, doesn’t wait for the page to load, will not detach the browser when the script ends, and the instance ID will be stored in the `♥id` variable:

```G1ANT
ie.open g1ant.com nowait true autodetachonclose false result ♥id
```


