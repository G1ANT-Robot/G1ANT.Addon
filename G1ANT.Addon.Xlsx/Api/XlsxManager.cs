using System;
using System.Collections.Generic;
using System.Linq;


namespace G1ANT.Addon.Xlsx
{
    public static class XlsxManager
    {
        private static List<XlsxWrapper> launchedXls = new List<XlsxWrapper>();

        public static XlsxWrapper CurrentXls { get; private set; }

        public static int getFirstId()
        {
            return launchedXls.First().Id;
        }

        private static int GetNextId()
        {
            return launchedXls.Count() > 0 ? launchedXls.Max(x => x.Id) + 1 : 0;
        }

        public static XlsxWrapper AddXls()
        {
            int assignedId = GetNextId();
            XlsxWrapper wrapper = new XlsxWrapper(assignedId);
            launchedXls.Add(wrapper);
            CurrentXls = wrapper;
            return wrapper;
        }

        public static bool Open(string filePath)
        {
            if (CurrentXls.Open(filePath)) return true;
            else return false;
        }

        public static int CountRows()
        {
            return CurrentXls.CountRows();
        }

        public static bool SwitchXls(int id)
        {
            var tmpXls = launchedXls.Where(x => x.Id == id).FirstOrDefault();
            CurrentXls = tmpXls ?? CurrentXls;
            return tmpXls != null;
        }

        public static void Remove(int id)
        {
            XlsxWrapper toRemove = launchedXls.Where(x => x.Id == id).FirstOrDefault();
            Remove(toRemove);
        }

        public static void Remove(XlsxWrapper wrapper)
        {
            if (wrapper != null)
            {
                wrapper.Close();
                if (launchedXls.Contains(wrapper))
                {
                    launchedXls.Remove(wrapper);
                }
                CurrentXls = launchedXls.FirstOrDefault();
            }
        }
    }
}
