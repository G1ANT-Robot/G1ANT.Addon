﻿/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using System;
using System.Collections.Generic;
using G1ANT.Language;
using G1ANT.Addon.Net.Properties;
using System.IO;
using System.Linq;

namespace G1ANT.Addon.Net
{
    [Addon(Name = "Net",
        Tooltip = "Net Commands")]
    [Copyright(Author = "G1ANT LTD", Copyright = "G1ANT LTD", Email = "hi@g1ant.com", Website = "www.g1ant.com")]
    [License(Type = "LGPL", ResourceName = "License.txt")]
    [CommandGroup(Name = "as400", Tooltip = "A command used to work with IBM AS/400 platform.")]
    [CommandGroup(Name = "is", Tooltip = "A command used for checking sth.")]
    [CommandGroup(Name = "mail",  Tooltip = "A command used with email")]
    [CommandGroup(Name = "rest",  Tooltip = "This command prepares a request to the desired url with selected method.")]
    [CommandGroup(Name = "vnc",  Tooltip = "A command connected with a VNC server.")]
    public class NetAddon : Language.Addon
    {
        public override void LoadDlls()
        {
            UnpackDrivers();
            base.LoadDlls();
        }

        private void UnpackDrivers()
        {
            var unpackfolder = AbstractSettingsContainer.Instance.UserDocsAddonFolder.FullName;
            var driversDictionary = new Dictionary<string, byte[]>()
            {
                { "putty.exe", Resources.putty },
                { "VNC.exe", Resources.VNC },
                { "telnet.exe", Resources.telnet },
            };
            foreach (var exe in driversDictionary.Where(e => !DoesFileExist(unpackfolder, e.Key) || AreFilesOfTheSameLength(e.Value.Length, unpackfolder, e.Key)))
            {
                try
                {
                    using (FileStream stream = File.Create(Path.Combine(unpackfolder, exe.Key)))
                    {
                        stream.Write(exe.Value, 0, exe.Value.Length);
                    }
                }
                catch (Exception ex) { RobotMessageBox.Show(ex.Message); }
            }
        }

        private bool DoesFileExist(string folder, string fileName)
        {
            return File.Exists(Path.Combine(folder, fileName));
        }

        private bool AreFilesOfTheSameLength(int lenght, string folder, string fileName)
        {
            return lenght != new FileInfo(Path.Combine(folder, fileName)).Length;
        }
    }
}