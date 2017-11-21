using System;
using System.Collections.Generic;
using System.IO;

using G1ANT.Engine;
using G1ANT.Interop;
using G1ANT.Language.Core.Tests;
using G1ANT.Language.Ocr.AbbyyFineReader.Commands;
using G1ANT.Language.Ocr.AbbyyFineReader.Structures;
using G1ANT.Language.Semantic;
using GStruct = G1ANT.Language.Structures;

using NUnit.Framework;
using System.Reflection;
using G1ANT.Language.Ocr.AbbyyFineReader.Tests.Properties;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Tests.Commands
{
    [TestFixture]
    [TestsClass(typeof(OcrAbbyyReadCell))]
    public class ReadCellTests
    {
        string path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.document3), "tif");

        [OneTimeSetUp]
        public void Initialize()
        {
            System.Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void ReadCellTest()
        {
            Scripter scripter = new Scripter();
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Text}{path}{SpecialChars.Text}");
            string egyptValue;
            scripter.RunLine($"ocrabbyy.readcell tableindex 1 position 8,3 result {nameof(egyptValue)}");
            egyptValue = scripter.Variables.GetVariableValue<string>(nameof(egyptValue));
            Assert.AreEqual("Egypt", egyptValue.Trim(), "Faild to retrive value from cell");

            string nigeriaValue;
            scripter.RunLine($"ocrabbyy.readcell tableindex 1 position 8,3 offset 1,0 result {nameof(nigeriaValue)}");
            nigeriaValue = scripter.Variables.GetVariableValue<string>(nameof(nigeriaValue));
            Assert.AreEqual("Nigeria", nigeriaValue.Trim(), "Faild to retrive value from cell using offset");
        }
    }
}
