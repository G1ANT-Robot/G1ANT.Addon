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
using G1ANT.Language;
using NUnit.Framework;
using System.Threading;

namespace G1ANT.Addon.Net.Tests
{
    [TestFixture]
    public class IsAccessibleTests
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
        [Test]
        public void IsAccessibleTest()
        {
            Scripter scripter = new Scripter();
            scripter.InitVariables.Clear();
            string hostname = "google.com";
            scripter.InitVariables.Add("hostname", new TextStructure(hostname));
            scripter.Text = ($"is.accessible hostname {SpecialChars.Variable}hostname");
            scripter.Run();

            bool result = scripter.Variables.GetVariableValue<bool>("result");
            if (!result)
            {
                Assert.Inconclusive("Very unlikely behaviour that google.com is not accessible");
            }
            Assert.AreEqual(true, result);
        }

        [Test]
        public void IsInaccessibleTest()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
            string hostname = "www.myapple.com";
           scripter.InitVariables.Add("hostname", new TextStructure(hostname));
            scripter.Text = ($"is.accessible hostname {SpecialChars.Variable}hostname timeout 1000");
            scripter.Run();

            bool result = scripter.Variables.GetVariableValue<bool>("result");
            if (result)
            {
                Assert.Inconclusive("Very unlikely behaviour that www.myapple.com is accessible");
            }
            Assert.AreEqual(false, result);
        }

        [Test]
        //[ExpectedException(typeof(ArgumentException))]
        public void BadHostTest()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
            string hostName = "ala.ma.kota.0a--e-eas=fo";
           scripter.InitVariables.Add("hostname", new TextStructure(hostName));

            scripter.Text = ($"is.accessible hostname {SpecialChars.Variable}hostname");

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<ArgumentException>(exception.GetBaseException());
        }
    }
}
