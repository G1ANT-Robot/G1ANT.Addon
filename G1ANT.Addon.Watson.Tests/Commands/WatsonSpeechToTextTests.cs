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
        private static string audioPath;
        private static Scripter scripter;
        private static string login = "1ab27db8-575a-4d3f-b6d0-49c744d2e9fb";
        private static string password = "3uKsggJu8hMc";

        [OneTimeSetUp]
        [Timeout(20000)]
        public static void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
             audioPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.SpeechTest), "wav");
            scripter = new Scripter();
scripter.InitVariables.Clear();
           scripter.InitVariables.Add("audioPath", new TextStructure(audioPath));
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
            WatsonSpeechToTextApi watson = new WatsonSpeechToTextApi(login, password);
            string res = watson.SpeechToText(audioPath, "en-US", 60000, 3, 0.2f);
            Assert.IsTrue(res.ToLower().Contains("hi"));
        }

        [Test]
        [Timeout(20000)]
        public void WatsonApiSpeachToTextTest2()
        {
            scripter.RunLine($"watson.speechtotext {SpecialChars.Variable}audioPath login {SpecialChars.Text}{login}{SpecialChars.Text} password {SpecialChars.Text}{password}{SpecialChars.Text}");
            var res = scripter.Variables.GetVariableValue<string>("result").ToLower().Trim();
            Assert.IsTrue(res.ToLower().Contains("hi"));
        }
    }
}
