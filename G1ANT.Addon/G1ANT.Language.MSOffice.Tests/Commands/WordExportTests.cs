
using G1ANT.Addon.MSOffice;


using System;
using System.IO;
using NUnit.Framework;
using System.Threading;
using System.Diagnostics;
using G1ANT.Engine;
using G1ANT.Language;

namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class WordExportTests
    {
        static Scripter scripter;

        private void KillProcesses()
        {
            foreach (Process p in Process.GetProcessesByName("word"))
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
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void WordExportTest()
        {
            string pdfPath = Environment.CurrentDirectory + @"\test.pdf";
            string xpsPath = Environment.CurrentDirectory + @"\test.xps";

            
            scripter.Variables.SetVariableValue("pdfPath", new TextStructure(pdfPath));
            scripter.Variables.SetVariableValue("xpsPath", new TextStructure(xpsPath));

            scripter.Text =
                    $@"word.open
                    word.export {SpecialChars.Variable}pdfPath
                    word.export {SpecialChars.Variable}xpsPath type xps
                    word.close";
            scripter.Run();

            FileInfo pdfFile = new FileInfo(pdfPath);
            FileInfo xpsFile = new FileInfo(xpsPath);

            Assert.IsTrue(pdfFile.Exists);
            Assert.IsTrue(xpsFile.Exists);

            pdfFile.Delete();
            xpsFile.Delete();
        }

        [TearDown]
        public void TestCleanUp()
        {
            Process[] proc = Process.GetProcessesByName("word");
            if (proc.Length != 0)
            {
                KillProcesses();
            }
        }
    }
}
