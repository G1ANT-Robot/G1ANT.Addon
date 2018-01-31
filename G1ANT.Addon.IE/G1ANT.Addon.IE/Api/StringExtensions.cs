using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.IExplorer
{
    public static class StringExtensions
    {
        public static string CapitalizeFirstLetter(this string text)
        {
            if (string.IsNullOrEmpty(text) == false)
            {
                return text.First().ToString().ToUpper() + text.Substring(1).ToLower();
            }
            else
            {
                return text;
            }
        }

    }
}
