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
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace G1ANT.Addon.IExplorer.Tests
{
    public static class IETests
    {
        public const int TestTimeout = 120000;

        public static void KillAllIeProcesses()
        {
            foreach (var ie in Process.GetProcessesByName("iexplore"))
            {
                try
                {
                    ie.Kill();
                }
                catch { }
            }
            Thread.Sleep(1000);
            foreach (var ie in Process.GetProcessesByName("iexplore"))
            {
                try
                {
                    ie.Kill();
                }
                catch { }
            }
        }

        public static int GetIeInstancesCount()
        {
            var processCount = Process.GetProcessesByName("iexplore").Count();
            return processCount > 0 ? processCount - 1 : 0;
        }

        public static List<Process> GetIeProcessesByTitle(params string[] keywords)
        {
            return Process.GetProcessesByName("iexplore")
                   .Where(x => keywords.All((x.MainWindowTitle?.ToLower() ?? string.Empty).Split(' ')
                   .Contains)).ToList();
        }

        public static void WaitForIeOpen(int timeoutMs, params string[] keywords)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.ElapsedMilliseconds < timeoutMs)
            {
                if (IETests.GetIeProcessesByTitle(keywords).Count > 0)
                {
                    return;
                }
            }
            throw new TimeoutException($"Internet explorer instance could not be found");
        }
    }
}
