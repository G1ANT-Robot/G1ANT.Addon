/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Watson
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

using System;
using System.Reflection;
using G1ANT.Addon.Watson.Tests.Properties;
using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;

namespace G1ANT.Addon.Watson.Tests.Commands
{
    [TestFixture]
    public class WatsonSpeechToTextTests
    {
        private static string audioPath;
        private Scripter scripter;
        private const string ApiKey = "https://gateway-lon.watsonplatform.net/speech-to-text/api";

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
            Language.Addon.Load(@"G1ANT.Addon.Watson.dll");
        }

        [Test]
        [Timeout(20000)]
        public void WatsonApiSpeachToTextTest2()
        {
            scripter.Text = $"watson.speechtotext path {SpecialChars.Variable}audioPath apikey {ApiKey}";
            scripter.Run();
            var res = scripter.Variables.GetVariableValue<string>("result").ToLower().Trim();
            Assert.IsTrue(res.ToLower().Contains("hi"));
        }
    }
}
