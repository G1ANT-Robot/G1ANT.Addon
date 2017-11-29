using System.Globalization;

namespace G1ANT.UiFramework.Utils
{
    public static class SystemLanguageRetreiver
    {
        public static CultureInfo GetCurrentOsCulture()
        {
            var currentOsCulture = CultureInfo.InstalledUICulture;
            return currentOsCulture;
        }
    }
}
