using G1ANT.Engine;
using G1ANT.Language.Core.Tests;
using G1ANT.Language.Ocr.AbbyyFineReader.Commands;
using G1ANT.Language.Ocr.AbbyyFineReader.Structures;
using G1ANT.Language.Semantic;
using GStructures = G1ANT.Language.Structures;

using System.Diagnostics;
using System.IO;
using System;
using NUnit.Framework;
using System.Reflection;
using G1ANT.Addon.Ocr.AbbyyFineReader.Tests.Properties;

namespace G1ANT.Addon.Ocr.AbbyyFineReader.Tests.Commands
{
    [TestFixture]
    [TestsClass(typeof(OcrAbbyyClose))]
    public class CloseTests
    {
        private static string path;
        private static Scripter scripter;

        [OneTimeSetUp]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [SetUp]
        public void SetUp()
        {
            path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.document1), "tif");
            scripter = new Scripter();
            scripter.Variables.SetVariableValue("file", new GStructures.String(path));
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        [TestsClass(typeof(OcrAbbyyProcessFile))]
        public void CloseTest()
        {
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Variable}file");
            int documentId = scripter.Variables.GetVariableValue<int>("result");
            FineReaderDocument document = AbbyyManager.Instance.GetDocument(documentId);
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Variable}file");
            long engineLoadedMemory = GetAllocatedMemory();
            scripter.RunLine($"ocrabbyy.close");
            long engineUnloadedMemory = GetAllocatedMemory();
            Assert.IsTrue(engineLoadedMemory - engineUnloadedMemory > 0x10000, $"Closing engine relesed less than 1MB of memory, relesed bytes = {engineLoadedMemory - engineUnloadedMemory}");
        }

        private static long GetAllocatedMemory()
        {
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
            return Process.GetCurrentProcess().WorkingSet64;
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        [TestsClass(typeof(OcrAbbyyProcessFile))]
        public void CloseDocumentTest()
        {
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Variable}file language English");
            int documentId = scripter.Variables.GetVariableValue<int>("result");
            FineReaderDocument document = AbbyyManager.Instance.GetDocument(documentId);
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Variable}file language English");
            int document2Id = scripter.Variables.GetVariableValue<int>("result");
            FineReaderDocument document2 = AbbyyManager.Instance.GetDocument(document2Id);

            scripter.RunLine($"ocrabbyy.close {document2Id}");
            document.ExtractData();
            bool isDocumentClosed = false;
            try
            {
                document2.ExtractData();
            }
            catch
            {
                isDocumentClosed = true;
            }
            Assert.IsTrue(isDocumentClosed);

            scripter.RunLine($"ocrabbyy.close {documentId}");
            isDocumentClosed = false;
            try
            {
                document.ExtractData();
            }
            catch
            {
                isDocumentClosed = true;
            }
            Assert.IsTrue(isDocumentClosed);
        }
    }
}
