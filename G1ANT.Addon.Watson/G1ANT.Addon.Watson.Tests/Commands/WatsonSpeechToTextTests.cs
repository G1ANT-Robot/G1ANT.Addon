/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Watson
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Addon.Watson.Tests.Properties;
using G1ANT.Engine;
using G1ANT.Language;
using G1ANT.Addon.Watson;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using G1ANT.Addon.Watson.Api;

namespace G1ANT.Addon.Watson.Tests
{
    [TestFixture]
    public class WatsonSpeechToTextTests
    {
        private static string audioPath;
        private Scripter scripter;

        [OneTimeSetUp]
        [Timeout(20000)]
        public void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            audioPath = Assembly.GetExecutingAssembly().UnpackResourceToFile("Resources." + nameof(Resources.SpeechTest), "wav");
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
            //WatsonSpeechToTextApi watson = new WatsonSpeechToTextApi((string)scripter.Variables.GetVariable("credential").GetValue("Watson:login").Object, (string)scripter.Variables.GetVariable("credential").GetValue("Watson:password").Object);
            //string res = watson.SpeechToText(audioPath, "en-US", 60000, 3, 0.2f);
            //Assert.IsTrue(res.ToLower().Contains("hi"));
        }

        [Test]
        [Timeout(20000)]
        public void WatsonApiSpeachToTextTest2()
        {
            scripter.Text = ($"watson.speechtotext {SpecialChars.Variable}audioPath login {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Watson:login{SpecialChars.IndexEnd} password {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Watson:password{SpecialChars.IndexEnd}");
            scripter.Run();
            var res = scripter.Variables.GetVariableValue<string>("result").ToLower().Trim();
            Assert.IsTrue(res.ToLower().Contains("hi"));
        }
    }
}
