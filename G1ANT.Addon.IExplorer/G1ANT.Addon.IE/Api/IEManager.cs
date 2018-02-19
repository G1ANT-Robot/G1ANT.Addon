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
using System.Collections.Generic;
using System.Linq;

namespace G1ANT.Addon.IExplorer
{
    public static class IEManager
    {
        private static IEWrapper currentIe = null;

        public static List<IEWrapper> launchedIE = new List<IEWrapper>();

        public static IEWrapper CurrentIE
        {
            get
            {
                if (currentIe == null)
                {
                    throw new InvalidOperationException("No Internet Explorer instance attached. Please, use either ie.open or ie.attach command first.");
                }
                return currentIe;
            }
            private set
            {
                currentIe = value;
            }
        } 

        public static bool SwitchIE(int id)
        {
            var tmpIE = launchedIE.Where(x => x.Id == id).FirstOrDefault();
            if (tmpIE != null)
            {
                CurrentIE = tmpIE;
            }
            return tmpIE != null;
        }

        private static int GetNextId()
        {
            return launchedIE.Count() > 0 ? launchedIE.Max(x => x.Id) + 1 : 0;
        }

        public static IEWrapper AddIE()
        {
            int assignedId = GetNextId();
            IEWrapper wrapper = new IEWrapper(assignedId);
            launchedIE.Add(wrapper);
            CurrentIE = wrapper;
            return wrapper;
        }

        public static void RemoveFromList(IEWrapper wrapper)
        {
            var toRemove = launchedIE.Where(x => x == wrapper).FirstOrDefault();
            if (toRemove != null)
            {
                launchedIE.Remove(toRemove);
            }
        }

        public static IEWrapper AttachToExistingIE(string name, string by)
        {
            int assignedId = GetNextId();
            IEWrapper wrapper = new IEWrapper(assignedId, name, by);
            launchedIE.Add(wrapper);
            CurrentIE = wrapper;
            return wrapper;
        }

        public static bool Close()
        {
            IEWrapper toClose = CurrentIE;
            if (toClose != null)
            {
                Detach(toClose);
                toClose.Close();
                return true;
            }
            return false;
        }

        public static void Detach(IEWrapper wrapper)
        {
            if (wrapper != null)
            {
                RemoveFromList(wrapper);
                if (CurrentIE == wrapper)
                {
                    CurrentIE = null;
                }
            }
        }
    }
}