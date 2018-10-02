# UI Addon concept



## Overview



UI Addon enables to automate windows desktop application and provides tools to enhance automation development process.



## Architecture



1. Engine

   1. Automation Engine

   2. XPath parser/generator

2. G1ANT Language Extension

   1. Commands

   2. Triggers

   3. Structures ?

   1. UI tree panel

   2. XPath wizard



## 1. Engine

Engine is responsible for interaction with OS and other application to retrieve information and perform automation tasks. It's also provide simple to use access to this functionality.



### 1.i Automatin Engine

Automation Engine performs automation task and retrieve information about ui elements. It's can be optimized as needed, to balance execution time and resource consumption. Provides API on level of actions like click or search an element by XPath an hides interactions with low-level APIs.



There are lots of different API's that engine could use. Wrapper around windows automation com library (https://archive.codeplex.com/?p=uiautomationverify https://github.com/TestStack/UIAComWrapper) are good starting point. Net Framework Windows.Automation (https://docs.microsoft.com/en-us/dotnet/api/system.windows.automation?view=netframework-4.7.2) and c++ win.32 (https://docs.microsoft.com/pl-pl/windows/desktop/WinAuto/windows-automation-api-overview) can be useful.



Engine works as facade to underling API. It has to be form of translation based on context of an actual UI element. Same element can support multiple action at the same time and different set of them depending on a state of application. Further expansion of handled application could require dealing with Accessible server (Legacy api introduced in win95). Accessible server is based on few different action and role enumeration.



Support for UI tree browsing can support element pattern recognition, that would allow diving into expandable elements or data grids. StructureChanged event can be used to get access to context menu or menu bar.



### 1.ii XPath parser/generator

Xpath parser transforms textual representation of a XPath an turns it into tree walk instructions, when Xpath generator do the opposite.



Parser that do not work in context of an actual ui tree allows for separation of instruction from traversing the tree. This lets optimize ui tree walking with no changes in parser.



## 2. G1ANT Language Extensions

Extensions are providing features to RPA developer. First kind of features are Robot functionalities like commands or triggers. Another one are development studio enhancements, like panels or wizards.



### 2.i Commands

Commads should be as simple as possible, but there might be need to add (hidden ?) parameters (resembling low level api) for uncommon scenarios which simple command cannot handle. There is a need for command to retrieve a state of an element (ex enabled).



### 2.ii Triggers

UI triggers can be useful to automate monitoring fo an application (ex. responding to a message in a communicator)



### 2.iv UI tree panel

Tree panel should be capable to get access to expandable elements (drop-downs menus), this might be accomplished by coupling invoking expand and subscribing to an treestructurechanged (need investigation)



The panel have to reliably present position of selected element and trace an element under mouse cursor position. Issue with scaling have to be solved (https://docs.microsoft.com/pl-pl/windows/desktop/WinAuto/uiauto-screenscaling)



### 2.v XPath wizard

Properties DropDowns with multiple selection for every tree level with predefined selected property depending on which are provided by an element. Properties selection means that element to be found have to have selected property equal to current element (there are properties that tells if specified pattern is supported). Multiple selection creates 'AndRule'. Detection of ambiguous path can be added (for current state).