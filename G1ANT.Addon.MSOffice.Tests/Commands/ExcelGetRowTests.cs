

using G1ANT.Addon.MSOffice.Tests.Properties;
using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExcelGetRowTests
    {
        Scripter scripter;
        static string xlsPath;

        private void KillProcesses()
        {
            foreach (Process p in Process.GetProcessesByName("excel"))
            {
                try
                {
                    p.Kill();
                }
                catch { }
            }
        }

        [OneTimeSetUp]
        public static void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            scripter = new Scripter();
scripter.InitVariables.Clear();
        }

        [SetUp]
        public void TestInit()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.MSOffice.dll");
            xlsPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.getRowTest), "xlsx");
           scripter.InitVariables.Add("xlsPath", new TextStructure(xlsPath));
            scripter.RunLine($"excel.open {SpecialChars.Variable}xlsPath");
        }
        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelGetRowTest()
        {//TODO GETROW Do not have dictionary as return value
            scripter.RunLine("excel.getrow row 1");
            Dictionary<string,TextStructure> dictionary = (Dictionary<string, TextStructure>)scripter.Variables.GetVariable("result").GetValue().Object;
            Assert.AreEqual("1".ToString(CultureInfo.CurrentCulture), (dictionary["b"].Object));
            Assert.AreEqual("abc".ToString(CultureInfo.CurrentCulture), (dictionary["c"].Object));
            Assert.AreEqual(double.Parse("1.53", NumberStyles.Any, CultureInfo.CurrentCulture).ToString(), double.Parse((dictionary["d"] as TextStructure).Value, NumberStyles.Any, CultureInfo.CurrentCulture).ToString()); 
            Assert.AreEqual(DateTime.Parse("2-Feb", CultureInfo.CurrentCulture).ToString(CultureInfo.CurrentCulture), DateTime.Parse((dictionary["f"] as TextStructure).Value, CultureInfo.CurrentCulture).ToString(CultureInfo.CurrentCulture));
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelGetRowFailTest()
        {

            scripter.Text = "excel.getrow row 0";
            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<ArgumentException>(exception.GetBaseException());
        }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.RunLine("excel.close");
            Process[] proc = Process.GetProcessesByName("excel");
            if (proc.Length != 0)
            {
                KillProcesses();
            }
        }
    }
}
