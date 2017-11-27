using System;
using System.Collections.Generic;
using System.IO;

using G1ANT.Engine;
using GStruct = G1ANT.Language;

using NUnit.Framework;
using System.Reflection;
using G1ANT.Addon.Ocr.AbbyyFineReader.Tests.Properties;
using G1ANT.Language;

namespace G1ANT.Addon.Ocr.AbbyyFineReader.Tests
{
    [TestFixture]
    public class ReadCellTests
    {
        string path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.document3), "tif");

        [OneTimeSetUp]
        public void Initialize()
        {
            System.Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }
        [SetUp]
        public void Init()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Ocr.AbbyyFineReader.dll");
        }
        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void ReadCellTest()
        {
            Scripter scripter = new Scripter();
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Text}{path}{SpecialChars.Text}");
            string egyptValue;
            scripter.RunLine($"ocrabbyy.readcell tableindex 1 position 8,3 result {SpecialChars.Variable}{nameof(egyptValue)}");
            egyptValue = scripter.Variables.GetVariableValue<string>(nameof(egyptValue));
            Assert.AreEqual("Egypt", egyptValue.Trim(), "Faild to retrive value from cell");

            string nigeriaValue;
            scripter.RunLine($"ocrabbyy.readcell tableindex 1 position 8,3 offset 1,0 result {SpecialChars.Variable}{nameof(nigeriaValue)}");
            nigeriaValue = scripter.Variables.GetVariableValue<string>(nameof(nigeriaValue));
            Assert.AreEqual("Nigeria", nigeriaValue.Trim(), "Faild to retrive value from cell using offset");
        }
    }
}
