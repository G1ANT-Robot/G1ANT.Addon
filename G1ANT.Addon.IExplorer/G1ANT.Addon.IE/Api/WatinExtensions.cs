/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.IExplorer
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using System;
using WatiN.Core;

namespace G1ANT.Addon.IExplorer
{
    public static class WatinExtensions
    {

        public static bool IsJavascriptEvaluateTrue(this Browser browser, string javascript)
        {
            var evalResult = browser.Eval("if (" + javascript + ") {true;} else {false;}");
            return evalResult == "true";
        }

        public static string FindIdViaQuery(this Browser browser, string selector, string searchType, int timeoutMs)
        {
            int start = Environment.TickCount;
            while (Environment.TickCount - start <= timeoutMs)
            {
                if (!browser.IsJavascriptEvaluateTrue("typeof('WatinSearchHelper') == 'function'"))
                {
                    var js = IEWrapper.MsCrmFindViaQueryAllSelector.Replace("\n", "").Replace("\r", "");
                    browser.Eval(js);
                }
                var elementId = browser.Eval(string.Format($"WatinSearchHelper.getElementId('{selector}', '{searchType}')"));

                if (elementId != "_no_element_" && !string.IsNullOrEmpty(elementId))
                {
                    return elementId;
                }
            }
            throw new TimeoutException($"Unable to find element on page with selector '{selector}' of type '{searchType}'");
        }
    }
}
