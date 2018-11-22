# ie.waitforvalue

**Syntax:**

```G1ANT
ie.waitforvalue  script ‴‴  expectedvalue ‴‴
```

**Description:**

Command `ie.waitforvalue` allows to execute script on the current opened webpage and waits for this script to return expected value.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`script`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes|  | pass the full script as string to get it evaluated in browser |
|`expectedvalue`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes |  | value which we expect that script will return  |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutie](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.IExplorer.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer](https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer)

**Example 1:**

This example opens Internet Explorer and loads 'https://g1ant.com/' webpage using `ie.open` command. Then we wait for script  'document.getElementsByClassName("copyright").length' to get value different than null. Finally, when the chosen element on a webpage loads, G1ANT.Robot will close IE.

```G1ANT
ie.open url ‴https://g1ant.com/‴
ie.waitforvalue script ‴document.getElementsByClassName("copyright").length‴ expectedvalue ‴1‴
dialog message ‴true‴
ie.close
```

This is the HTML element we are waiting for to load. G1ANT.Robot uses the fact that the execution of a webpage is top down and single threaded. An element will only be available in the DOM after it has been parsed, so when one of the last elements is loaded, we can assume the web page has fully loaded.

When the `expectedvalue` argument reaches ‴1‴, G1ANT.Robot executes another command- `dialog` and closes the browser.

**Example 2:**

In this example G1ANT.Robot will open a website and `ie.waitforvalue` command waits for a certain HTML DOM element (that we choose using jquery) to load. In our case `.length` of the `$(".catphoto")` has to have `expectedvalue` argument set to '1', which means it is loaded.

```G1ANT
ie.open url ‴http://www.funnycatpix.com/‴
ie.waitforvalue script ‴$(".catphoto").length‴ expectedvalue ‴1‴ errormessage ‴can't load‴
ie.gettitle result ♥title
dialog ♥title
```
