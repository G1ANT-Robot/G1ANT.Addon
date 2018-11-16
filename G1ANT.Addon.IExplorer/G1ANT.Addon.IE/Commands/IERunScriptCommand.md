# ie.runscript

**Syntax:**

```G1ANT
ie.runscript  script ‴‴
```

**Description:**

Command `ie.runscript` allows to execute script on the currently attached Internet Explorer instance. After successful evaluation you can get the result from a variable.
Please be aware that only the first called function of the script might be evaluated as a result and if you want to get multiple results, compose script which returns all values as json or another single string solution in one function.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`script`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | yes|  | pass the full script as string to get it evaluated in browser |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | name of variable where the result of javascript execution will be stored |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutie](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.IExplorer.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer](https://github.com/G1ANT-Robot/G1ANT.Addon.IExplorer)

**Example 1:**

```G1ANT
ie.open
ie.runscript ‴alert("Hello world!");‴
```

**Example 2:**

If your script is more complex, it is worth loading it from file to avoid any errors. Keep in mind that only result of the first function will be assigned to **result** in G1ANT.Language so nest your operations inside the first function.

Content of test.js

```G1ANT
callMe();
alert(thisWillNotReturn());
thisWillNotReturn();
function callMe(){
	var result = "I'm returned to G1ANT Robot :) ";
	result += nestedFunc();
	result += nestedFunc2();
	return result;
}
function nestedFunc(){
	return "lol1 ";
}
function nestedFunc2(){
	return "lol2 ";
}
function thisWillNotReturn(){
	return "I stay here :( ";
}
```

```G1ANT
text.read filename ‴C:\Users\dell\Documents\G1ANT.Robot\test.js‴ result ♥script
ie.open
ie.runscript ♥script
dialog ♥result
```

**Example 3:**

```G1ANT
ie.open
ie.seturl ‴google.pl‴
ie.runscript script ‴x=2 + 5‴ result ♥x
dialog ♥x
```

**Example 4:**

```G1ANT
ie.open
ie.seturl ‴google.pl‴
ie.runscript script ‴el = document.createElement('div');el.id = 'hehe';document.body.appendChild(el);‴
ie.getattribute name ‴id‴ search ‴hehe‴ by id timeout 2000 result ♥atrName
dialog ♥atrName
```
