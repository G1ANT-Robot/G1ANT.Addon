using System;
using System.Collections.Generic;
using System.Linq;


namespace G1ANT.Language.Xls
{
    public static class XlsManager
    {
        private static List<XlsWrapper> launchedXls = new List<XlsWrapper>();

        public static XlsWrapper CurrentXls { get; private set; }

        public static int getFirstId()
        {
            return launchedXls.First().Id;
        }

        private static int GetNextId()
        {
            return launchedXls.Count() > 0 ? launchedXls.Max(x => x.Id) + 1 : 0;
        }

        public static XlsWrapper AddXls()
        {
            int assignedId = GetNextId();
            XlsWrapper wrapper = new XlsWrapper(assignedId);
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
            XlsWrapper toRemove = launchedXls.Where(x => x.Id == id).FirstOrDefault();
            Remove(toRemove);
        }

        public static void Remove(XlsWrapper wrapper)
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
