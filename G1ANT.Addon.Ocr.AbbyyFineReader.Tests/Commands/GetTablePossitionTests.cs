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
using G1ANT.Addon.Ocr.AbbyyFineReader.Tests.Properties;

namespace G1ANT.Addon.Ocr.AbbyyFineReader.Tests.Commands
{
    [TestFixture]
    [TestsClass(typeof(OcrAbbyyGetTablePosition))]
    public class GetTablePossitionTests
    {
        string path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.document3), "tif");

        [OneTimeSetUp]
        public void Initialize()
        {
            System.Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void GetTablePositionTest()
        {
            Scripter scripter = new Scripter();
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Text}{path}{SpecialChars.Text}");

            string egyptPosition;
            scripter.RunLine($"ocrabbyy.gettableposition Egypt tableindex 0 result {nameof(egyptPosition)}");
            egyptPosition = ((GStruct.String)scripter.Variables.GetVariableValue<List<GStruct.Structure>>(nameof(egyptPosition))[0]).Value;
            string NigeriaPosition;
            scripter.RunLine($"ocrabbyy.gettableposition Nigeria tableindex 0 result {nameof(NigeriaPosition)}");
            NigeriaPosition = ((GStruct.String)scripter.Variables.GetVariableValue<List<GStruct.Structure>>(nameof(NigeriaPosition))[0]).Value;
            Assert.AreEqual("8,3", egyptPosition);
            Assert.AreEqual("9,3", NigeriaPosition);
        }
    }
}
