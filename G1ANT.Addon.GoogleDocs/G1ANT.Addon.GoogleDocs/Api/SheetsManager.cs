using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.GoogleDocs
{
    public static class SheetsManager
    {
        private static List<SheetsWrapper> launchedSheets = new List<SheetsWrapper>();
        public static SheetsWrapper CurrentSheet { get; private set; }
        public static bool SwitchSheet(int id)
        {
            var tmpSheet = launchedSheets.Where(x => x.Id == id).FirstOrDefault();
            CurrentSheet = tmpSheet ?? CurrentSheet;
            return tmpSheet != null;
        }
        public static bool SwitchSheet(string title)
        {
            var tmpSheet = launchedSheets.Where(x => x.spreadSheetName.Contains(title)).FirstOrDefault();
            CurrentSheet = tmpSheet ?? CurrentSheet;
            return tmpSheet != null;
        }
        private static int GetNextId()
        {
            return launchedSheets.Count() > 0 ? launchedSheets.Max(x => x.Id) + 1 : 0;
        }
        public static SheetsWrapper AddSheet()
        {
            int assignedId = GetNextId();
            SheetsWrapper wrapper = new SheetsWrapper(assignedId);
            launchedSheets.Add(wrapper);
            CurrentSheet = wrapper;
            return wrapper;
        }
        public static void Remove(int id)
        {
            var toRemove = launchedSheets.Where(x => x.Id == id).FirstOrDefault();
            launchedSheets.Remove(toRemove);
        }
        public static void Remove(SheetsWrapper wrapper)
        {
            var toRemove = launchedSheets.Where(x => x == wrapper).FirstOrDefault();
            if (toRemove != null)
            {
                Remove(toRemove.Id);
            }
        }
        public static void Remove(string spreadSheetId)
        {
            var toRemove = launchedSheets.Where(x => x.GetSpreadSheetId() == spreadSheetId).FirstOrDefault();
            if (toRemove != null)
            {
                Remove(toRemove.Id);
            }
        }
    }
}
