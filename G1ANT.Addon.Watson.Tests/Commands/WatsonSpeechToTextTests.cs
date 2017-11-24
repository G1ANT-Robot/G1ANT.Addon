using G1ANT.Addon.Watson.Tests.Properties;
using G1ANT.Engine;
using G1ANT.Language;
using G1ANT.Addon.Watson;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace G1ANT.Addon.Watson.Tests
{
    [TestFixture]
    public class WatsonSpeechToTextTests
    {
        static string audioPath;
        static Scripter scripter;

        [OneTimeSetUp]
        [Timeout(20000)]
        public static void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
             audioPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.SpeechTest), "wav");
            scripter = new Scripter();
            scripter.Variables.SetVariableValue("audioPath", new TextStructure(audioPath));
        }
        [SetUp]
        public void Init()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Watson.dll");
        }
        [Test]
        [Timeout(20000)]
        public void WatsonApiSpeechToTextTest()
        {
            WatsonSpeechToTextApi watson = new WatsonSpeechToTextApi();
            string res = watson.SpeechToText(audioPath, "en-US", 60000, 3, 0.2f);
            Assert.IsTrue(res.ToLower().Contains("hi"));
        }

        [Test]
        [Timeout(20000)]
        public void WatsonApiSpeachToTextTest2()
        {
            scripter.RunLine($"watson.speechtotext {SpecialChars.Variable}audioPath");
            var res = scripter.Variables.GetVariableValue<string>("result").ToLower().Trim();
            Assert.IsTrue(res.ToLower().Contains("hi"));
        }
    }
}
