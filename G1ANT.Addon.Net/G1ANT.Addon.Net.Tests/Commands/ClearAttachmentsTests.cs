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
using NUnit.Framework;
using Moq;
using G1ANT.Engine;
using G1ANT.Language.Services;
using G1ANT.Language;

namespace G1ANT.Addon.Net.Tests
{
    [TestFixture]
    public class ClearAttachmentsTests
    {
        private Mock<ILongLivingTempFileService> longLivingTempFileServiceMock;
        Scripter scripter;
        MailClearAttachmentsCommand sut;

        [OneTimeSetUp]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [SetUp]
        public void Init()
        {
            longLivingTempFileServiceMock = new Mock<ILongLivingTempFileService>();
            scripter = new Scripter();
            sut = new MailClearAttachmentsCommand(scripter,longLivingTempFileServiceMock.Object);
        }

        [TestFixture]
        public class ExecuteTests : ClearAttachmentsTests
        {
            
            [Test]
            public void ShouldCallDeleteAllTempFilesWithPrefixMethod()
            {
                ArgumentsBase argumentsBase = new ArgumentsBase();
                longLivingTempFileServiceMock.Setup(c => c.DeleteAllTempFilesWithPrefix(It.IsAny<string>()));
                sut.Execute(argumentsBase);
                longLivingTempFileServiceMock.VerifyAll();
            }
        }
    }
}
