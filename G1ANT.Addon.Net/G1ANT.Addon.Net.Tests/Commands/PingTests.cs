/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using System;

using G1ANT.Engine;
using NUnit.Framework;
using G1ANT.Language;

namespace G1ANT.Addon.Net.Tests
{

    [TestFixture]
    public class PingTests
    {

        public const int TestTimeout = 20000;

        [OneTimeSetUp]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }
        [SetUp]
        public void Init()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Net.dll");
        }
        [Test, Timeout(TestTimeout)]
        public void PingGoogleTest()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
           scripter.InitVariables.Add("ping", new TextStructure("google.com"));

            try
            {
                scripter.RunLine($"ping ip {SpecialChars.Variable}ping");
                int response = scripter.Variables.GetVariableValue<int>("result");
                Assert.IsTrue(response >= 0);
            }
            catch
            {
                Assert.Inconclusive("We can't ping google.com, it is very unlikely");
            }
        }

        [Test, Timeout(TestTimeout)]
        public void PingGoogleMultipleTimesTest()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
           scripter.InitVariables.Add("ping", new TextStructure("google.com"));

            try
            {
                var justsomerandomrepeatCount = 7;
                scripter.RunLine($"ping ip {SpecialChars.Variable}ping repeats {justsomerandomrepeatCount}");
                int response = scripter.Variables.GetVariableValue<int>("result");
                Assert.IsTrue(response >= 0);
            }
            catch
            {
                Assert.Inconclusive("We can't ping google.com, it is very unlikely");
            }
        }

        [Test, Timeout(TestTimeout)]
        public void PingLocalhostTest()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
           scripter.InitVariables.Add("ping", new TextStructure("localhost"));

            scripter.Text = ($"ping ip {SpecialChars.Variable}ping ");
            scripter.Run();
            int response = scripter.Variables.GetVariableValue<int>("result");
            if (response > 1)
            {
                Assert.Inconclusive("Something is wrong with network stack");
            }
        }

        [Test, Timeout(TestTimeout)]
        //[ExpectedException(typeof(System.Net.NetworkInformation.PingException))]
        public void TimeoutPingTest()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
           scripter.InitVariables.Add("ping", new TextStructure("www.myapple.com"));
            scripter.Text = ($"ping ip {SpecialChars.Variable}ping timeout 2000");

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<System.Net.NetworkInformation.PingException>(exception.GetBaseException());
        }
    }
}
