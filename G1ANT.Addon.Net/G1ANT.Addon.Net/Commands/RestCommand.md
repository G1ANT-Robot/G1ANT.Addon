# rest

## Syntax

```G1ANT
rest method ⟦text⟧ headers ⟦list⟧ parameters ⟦list⟧
```

## Description

This command prepares a request to a desired URL with a selected method. Multiple parameters can be passed and all of them will be attached as a request body.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`method`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | HTTP method of the `rest` request: `post` or `get` |
|`url`| [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes|  | URL of API method |
|`headers`| [list](G1ANT.Language/G1ANT.Language/Structures/ListStructure.md) | no |  |Headers attached to the request. Separate headers using ❚ character (**Ctrl+\\**); their keys and values should be separated with colon (:) |
|`parameters`| [list](G1ANT.Language/G1ANT.Language/Structures/ListStructure.md) | no |  | Parameters attached to the request. Separate headers using ❚ character (**Ctrl+\\**); their keys and values should be separated with colon (:) |
|`result`| [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no |  ♥result  |Name of a variable which will store the data returned by the API (usually json or xml) |
|`status`| [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no | ♥status |Name of a variable which will store the request delivery status  |
| `if`           | [bool](G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
|`timeout`| [timespan](G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no | [♥timeoutrest](G1ANT.Addon/G1ANT.Addon.Net/G1ANT.Addon.Net/Variables/TimeoutRestVariable.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](G1ANT.Manual/appendices/common-arguments.md) page.

## Example

In this example lists with headers and parameters are created and then used as values for their respective arguments in the request. The returned request result is then displayed in the first dialog box, and the request status in the second one.

> **Note:** It’s not possible to specify just one header or parameter in the `rest` command — a list must be provided, even if it contains only one element.

```G1ANT
list.create type1:xml result ♥list1
list.create par1:value result ♥list2

rest get url https://httpbin.org/get headers ♥list1 parameters ♥list2
dialog ♥result
dialog ♥status
```

