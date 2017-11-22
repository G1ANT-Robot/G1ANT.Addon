using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace G1ANT.Language.Mscrm
{
    public static class MsCrmManager
    {
        private static List<MsCrmWrapper> launchedCRM = new List<MsCrmWrapper>();

        public static MsCrmWrapper CurrentCRM { get; private set; }

        public static bool SwitchCRM(int id)
        {
            var tmpCRM = launchedCRM.Where(x => x.Id == id).FirstOrDefault();
            CurrentCRM = tmpCRM ?? CurrentCRM;
            return tmpCRM != null;
        }

        public static List<MsCrmWrapper> GetLaunchedCRM()
        {
            return launchedCRM;
        }

        private static int GetNextId()
        {
            return launchedCRM.Count() > 0 ? launchedCRM.Max(x => x.Id) + 1 : 0;
        }

        public static MsCrmWrapper AddCRM()
        {
            int assignedId = GetNextId();
            MsCrmWrapper wrapper = new MsCrmWrapper(assignedId);
            launchedCRM.Add(wrapper);
            CurrentCRM = wrapper;
            return wrapper;
        }

        public static MsCrmWrapper AttachToExistingCRM(string name, string by, bool msCrmRecorder = false)
            //int assignedId = GetNextId();
            //IEWrapper wrapper = new IEWrapper(assignedId, name, by);
            //launchedIE.Add(wrapper);
            //CurrentIE = wrapper;
            //return wrapper;
        {
            int assignedId = GetNextId();
            
            MsCrmWrapper wrapper = new MsCrmWrapper(assignedId, name, by, msCrmRecorder);
            launchedCRM.Add(wrapper);
            CurrentCRM = wrapper;
            return wrapper;
        }

        public static void FindAnyActiveCRM()
        {
            int assignedId = GetNextId();

            IntPtr iHandle = FindWindow("✱Internet Explorer✱", 3000);
            if (iHandle != IntPtr.Zero)
            { 
                MsCrmWrapper wrapper = new MsCrmWrapper(assignedId, "crm", "url");
                if (wrapper != null)
                {
                    launchedCRM.Add(wrapper);
                    CurrentCRM = wrapper;
                }                
                else
                {
                    throw new ApplicationException("Specified CRM window not found");
                }
            }
            else
            {
                throw new ApplicationException("Specified CRM window not found");
            }
        }

        private static IntPtr FindWindow(string name, int timeout)
        {
            Process[] list = Process.GetProcessesByName(name);
            if (list.Length > 0)
            {
                return list[0].Handle;
            }
            return new IntPtr(-1);
        }

        public static void Detach(MsCrmWrapper wrapper)
        {
            var toRemove = launchedCRM.Where(x => x == wrapper).FirstOrDefault();
            if (toRemove != null)
            {
                launchedCRM.Remove(toRemove);
            }
            if (CurrentCRM == wrapper)
            {
                CurrentCRM = null;
            }
        }
    }

}
