using G1ANT.Engine;
using G1ANT.Language.Core.Tests;
using G1ANT.Language.Ocr.Tesseract.Tests.Properties;
using G1ANT.Language.Semantic;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace G1ANT.Language.Ocr.Tesseract.Tests.Commands
{
    [TestFixture]
    [TestsClass(typeof(OcrOfflineFromScreenTests))]
    public class OcrOfflineFromScreenTests
    {
        private static Scripter scripter;
        private Process proces;
        private static string filePath = string.Empty;

        [OneTimeSetUp]
        public static void OcrOfflineTestsInitialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            filePath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.testimageDance), "png");
        }

        [SetUp]
        public void OcrFromScreenInitialize()
        {
            scripter = new Scripter();
            proces = Process.Start("mspaint.exe", $"\"{filePath}\"");
            proces.WaitForInputIdle();
            Thread.Sleep(3000);

        }

        [Test]
        [Apartment(ApartmentState.STA)]
        [Timeout(12000)]
        public void OcfOfflineFromScreenCommandTest()
        {
            // window {SpecialChars.Search}Paint{SpecialChars.Search} style maximize
            string script = $@"
            ocroffline.fromscreen area (rectangle)68,162,767,528
            ";
            scripter.Text = script;
            scripter.Run();
            var resultOutput = scripter.Variables.GetVariableValue<string>("result");
            Assert.IsTrue(resultOutput.Contains("Dance"));
            Assert.IsTrue(resultOutput.Contains("till"));
            Assert.IsTrue(resultOutput.Contains("the"));
            Assert.IsTrue(resultOutput.Contains("DAWN"));
        }

        [TearDown]
        public void OcrFromScreenCleanup()
        {
            if (proces != null && !proces.HasExited)
            {
                proces.Kill();
            }
        }

    }
}