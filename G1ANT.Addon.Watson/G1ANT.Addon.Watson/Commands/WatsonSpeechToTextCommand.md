# watson.speechtotext

**Syntax:**

```G1ANT
watson.speechtotext  path ‴‴  login ‴‴  password ‴‴
```

**Description:**

Command `watson.speechtotext` allows to transcript speech from audio file.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`path`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md)  | yes |  | specifies path to file with speech recorded |
|`login`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md)  | yes |  | specifies service's login | 
|`password`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md)  | yes |  | specifies service's password | 
|`result`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md)  | no |  [♥result](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  | returns most likely text transcription of the speech, recognised by Artificial Intelligence | 
|`threshold`| [float](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/float.md)  | no | | floating point value that specifies the minimum score a class must have to be displayed in the results, between 0 and 1 | 
|`language`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md)  | no | | specifies language of speech |
|`if`| [bool](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/bool.md) | no | true | runs the command only if condition is true |
|`timeout`| [variable](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Special-Characters/variable.md) | no | [♥timeoutwatson](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Variables/Special-Variables.md) | specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
|`errorjump` | [label](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/label.md) | no | | name of the label to jump to if given `timeout` expires |
|`errormessage`| [string](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Structures/string.md) | no |  | message that will be shown in case error occurs and no `errorjump` argument is specified |

For more information about `if`, `timeout`, `errorjump` and `errormessage` arguments, please visit [Common Arguments](https://github.com/G1ANT-Robot/G1ANT.Manual/blob/master/G1ANT-Language/Common-Arguments.md)  manual page.

This command is contained in **G1ANT.Addon.Watson.dll**.
See: [https://github.com/G1ANT-Robot/G1ANT.Addon.Watson](https://github.com/G1ANT-Robot/G1ANT.Addon.Watson)

**Generating login and password:**

In order to generate the required arguments- **login** and **password** for `watson.speechtotext` command, we need to follow the instructions below:

1. Log in to IBM Watson account using previously created account. In order to see how to create an account, please go to "watson commands":{TOPIC-LINK+watson-commands}

2. Press **Create resource** button

3. Press **Speech to Text** option

4. Choose the plan and press **Create** button

5. Go to dashboard using link 'https://console.bluemix.net/dashboard/apps'
6.  Hover over **Speech to Text** and press it

7. Go to **Service credentials** on the left side menu tab
8. Press **New credential** button

9. Press **Add** button

10. Roll out the **View credentials** menu in the bottom

11. You can now see the **username** and **password** that can be used as login and password in `watson.speechtotext` command

**Example 1:**

```G1ANT
watson.speechtotext path ‴C:\SomeFolder\speechFile.wav‴ language ‴en-US‴ threshold 0.6
```
