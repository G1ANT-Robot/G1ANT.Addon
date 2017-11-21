﻿using G1ANT.Language.Selenium.Api;
using System.Diagnostics;
using System.Linq;

namespace G1ANT.Language.Selenium.Tests
{
    public static class SeleniumTests
    {
        public const int TestTimeout = 1200000;

        public static void KillAllBrowserProcesses()
        {
            FirefoxKillAllProcesses();
            ChromeKillAllProcesses();
            EdgeKillAllProcesses();
            IeKillAllProcesses();
            SeleniumManager.DisposeAllOpenedDrivers();
        }
        public static void FirefoxKillAllProcesses()
        {
            foreach (var ie in Process.GetProcessesByName("firefox"))
            {
                try
                {
                    ie.Kill();
                }
                catch { }                
            }
        }
        public static void ChromeKillAllProcesses()
        {
            foreach (var ie in Process.GetProcessesByName("chrome"))
            {
                try
                {
                    ie.Kill();
                }
                catch { }
            }
        }
        public static void EdgeKillAllProcesses()
        {
            foreach (var ie in Process.GetProcesses().Where(x => x.ProcessName.Contains("Edge")))
            {
                try
                {
                    ie.Kill();
                }
                catch { }
            }
        }
        public static void IeKillAllProcesses()
        {
            foreach (var ie in Process.GetProcessesByName("iexplore"))
            {
                try
                {
                    ie.Kill();
                }
                catch { }
            }
        }

        public static int IeGetInstancesCount()
        {
            return Process.GetProcesses().
                Where(x => !string.IsNullOrEmpty(x.MainWindowTitle) && x.MainWindowTitle.
                Contains("Internet Explorer")).Count();
        }
        public static int ChromeGetInstancesCount()
        {
            return Process.GetProcesses().
                Where(x => !string.IsNullOrEmpty(x.MainWindowTitle) && x.MainWindowTitle.
                Contains("Chrome")).Count();
        }
        public static int EdgeGetInstancesCount()
        {
            return Process.GetProcesses().
                Where(x => !string.IsNullOrEmpty(x.MainWindowTitle) && x.MainWindowTitle.
                Contains("Edge")).Count();
        }
        public static int FirefoxGetInstancesCount()
        {
            return Process.GetProcesses().
                Where(x => !string.IsNullOrEmpty(x.MainWindowTitle) && x.MainWindowTitle.
                Contains("Firefox")).Count();
        }
    }
}