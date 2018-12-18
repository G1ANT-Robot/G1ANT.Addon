# watson.classifyimage

**Syntax:**

```G1ANT
watson.classifyimage  rectangle ‴‴  apikey ‴‴
```

**Description:**

Command `watson.classifyimage` allows to capture part of the screen and classify the image that was captured. Class scores range from 0 - 1, where a higher score indicates greater likelihood of the class being depicted in the image.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`rectangle`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md)  | yes |  | specifies capture screen area in format ‴x0//y0//x1//y1‴ |
|`apikey`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md)  | yes |  | specifies api key needed to login to the service |
|`relative`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true  | if set to true, rectangle's position relates to currently focused window|
|`threshold`| [float](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/float.md) | no | 0 | floating point value that specifies the minimum score a class must have to be displayed in the results, between 0 and 1 |
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | returns semicolon separated pairs of classes and score values, recognised by Artificial Intelligence|
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutwatson](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Watson.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Watson](https://github.com/G1ANT-Robot/G1ANT.Addon.Watson)

**Generating apikey**

In order to generate the required argument- apikey for `watson.classifyimage` command, we need to follow the instructions below:

1. Log in to IBM Watson account under this link: 'https://console.bluemix.net/login' using previously created account. In order to see how to create an account, please go to "watson commands":{TOPIC-LINK+watson-commands}.
2. Press **create resource** button

3. Hover over and press **Visual Recognition** option

4. Choose **PLAN**

5. Press **Create** button

6.Go to **Service credentials** on the left side menu tab

7. Press **New credential** button

8.Press **Add** button

9. Press **view credential**
10. Now you can see an **apikey** that can be used in `watson.classifyimage` command

**Example 1:**

```G1ANT
watson.classifyimage rectangle ‴480⫽244⫽1439⫽848‴ relative false
```

results in below classification:
orange:0,911;citrus:0,927;fruit:0,927;navel orange:0,748;vitamin:0,554;orange color:1;

**Example 2:**

```G1ANT
watson.classifyimage rectangle ‴480⫽244⫽1439⫽848‴ relative false threshold 0.9
```

results in below classification:
owl:0,961;bird of prey:0,962;bird:0,962;animal:0,962;
