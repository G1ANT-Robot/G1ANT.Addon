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
using System.Drawing;
using System.Threading.Tasks;
using G1ANT.Addon.Watson.Tests.Properties;
using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;

namespace G1ANT.Addon.Watson.Tests.Commands
{
    [TestFixture]
    public class WatsonClassifyImageTests
    {
        private Bitmap oranges = null;

        [SetUp]
        public void Init()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Watson.dll");
        }

        [OneTimeSetUp]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            oranges = Resources.oranges;
        }
        
        [Test]
        public void WatsonClassifyImageTimeout()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
            scripter.Text = $@"watson.classifyimage 27{SpecialChars.Point}0{SpecialChars.Point}194{SpecialChars.Point}27 timeout 1 apikey {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Watson:apikey{SpecialChars.IndexEnd}";

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<TaskCanceledException>(exception.GetBaseException());
        }

        [Test]
        public void WatsonWrongPositionTest()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
            scripter.Text = $@"watson.classifyimage 27{SpecialChars.Point}0{SpecialChars.Point}27{SpecialChars.Point}50 apikey {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Watson:apikey{SpecialChars.IndexEnd}";
            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<ApplicationException>(exception.GetBaseException());
        }
    }
}