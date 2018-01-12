using System;
using System.Collections.Generic;

using G1ANT.Engine;
using NUnit.Framework;
using G1ANT.Language;

namespace G1ANT.Addon.Net.Tests
{
    [TestFixture]
    public class RestTests
    {
        public const int TestTimeout = 40000;

        [OneTimeSetUp, Timeout(TestTimeout)]
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
        public void SendDataTest()
        {
            string url = "https://httpbin.org/post";

            ListStructure list = new ListStructure(new List<Structure>()
            {
                new TextStructure("something:val")
            });

            Scripter scripter = new Scripter();
           scripter.InitVariables.Add("url", new TextStructure(url));
           scripter.InitVariables.Add("method", new TextStructure("post"));
           scripter.InitVariables.Add("list", list);
            scripter.RunLine($"rest method {SpecialChars.Variable}method url {SpecialChars.Variable}url parameters {SpecialChars.Variable}list timeout {TestTimeout}");
            scripter.RunLine($"json {SpecialChars.Variable}result jpath ['form']['something']");

            Assert.AreEqual("val", scripter.Variables.GetVariableValue<string>("result"));
            Assert.AreEqual("Completed", scripter.Variables.GetVariableValue<string>("status"));
        }

        [Test, Timeout(TestTimeout)]
        public void SendDataWithHeadersTest()
        {
            string url = "https://httpbin.org/put";
            ListStructure parameters = new ListStructure(new List<Structure>()
            {
                new TextStructure("something:val")
            });

            ListStructure headers1 = new ListStructure(new List<Structure>()
            {
                new TextStructure("encoding-language:foo"),
                new TextStructure("version:3.0")
            });

            Scripter scripter = new Scripter();
           scripter.InitVariables.Add("params", parameters);

           scripter.InitVariables.Add("url", new TextStructure(url));
           scripter.InitVariables.Add("method", new TextStructure("put"));
           scripter.InitVariables.Add("headers", headers1);
            scripter.RunLine($"rest method {SpecialChars.Variable}method url {SpecialChars.Variable}url headers {SpecialChars.Variable}headers parameters {SpecialChars.Variable}params timeout {TestTimeout}");
            string resultJson = scripter.Variables.GetVariableValue<string>("result");

            scripter.RunLine($"json {SpecialChars.Variable}result jpath ['headers']['Encoding-Language'] result {SpecialChars.Variable}json1");
            Assert.AreEqual("foo", scripter.Variables.GetVariableValue<string>("json1"));

            scripter.RunLine($"json {SpecialChars.Variable}result jpath ['headers']['Version'] result {SpecialChars.Variable}json2");
            Assert.AreEqual("3.0", scripter.Variables.GetVariableValue<string>("json2"));

            url = "https://httpbin.org/get";

            ListStructure headers2 = new ListStructure(new List<Structure>()
            {
                new TextStructure("type1:xml")
            });

           scripter.InitVariables.Add("url", new Language.TextStructure(url));
           scripter.InitVariables.Add("method", new Language.TextStructure("get"));
           scripter.InitVariables.Add("headers", headers2);
            scripter.RunLine($"rest method {SpecialChars.Variable}method url {SpecialChars.Variable}url headers {SpecialChars.Variable}headers parameters {SpecialChars.Variable}params ");

            resultJson = scripter.Variables.GetVariableValue<string>("result");
            scripter.RunLine($"json {SpecialChars.Variable}result jpath ['headers']['Type1']");

            Assert.AreEqual("xml", scripter.Variables.GetVariableValue<string>("result"));
        }

        [Test, Timeout(TestTimeout)]
        //[ExpectedException(typeof(TimeoutException))]
        public void TimeoutTest()
        {
            string url = "http://validate.jsontest.com/";

            Scripter scripter = new Scripter();
           scripter.InitVariables.Add("url", new TextStructure(url));
            scripter.Text = ($"rest {SpecialChars.Text}get{SpecialChars.Text} url {SpecialChars.Variable}url timeout 1");

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<TimeoutException>(exception.GetBaseException());
        }

        [Test, Timeout(TestTimeout)]
        public void CompletedRequestStatusTest()
        {
            string url = "http://jsonplaceholder.typicode.com/posts/1";

            Scripter scripter = new Scripter();
           scripter.InitVariables.Add("method", new Language.TextStructure("delete"));
           scripter.InitVariables.Add("url", new Language.TextStructure(url));
            scripter.RunLine($"rest method {SpecialChars.Variable}method url {SpecialChars.Variable}url");

            Assert.AreEqual("Completed", scripter.Variables.GetVariableValue<string>("status"));
        }

        [Test, Timeout(TestTimeout)]
       // [ExpectedException(typeof(FormatException))]
        public void BadHeaderSeparatorTest()
        {
            string url = "http://jsonplaceholder.typicode.com/posts/1";

            ListStructure headers = new ListStructure(new List<Structure>()
            {
                new TextStructure("content-type(json")
            });

            Scripter scripter = new Scripter();
           scripter.InitVariables.Add("method", new TextStructure("put"));
           scripter.InitVariables.Add("url", new TextStructure(url));
           scripter.InitVariables.Add("headers", headers);
            scripter.Text = ($"rest method {SpecialChars.Variable}method url {SpecialChars.Variable}url headers {SpecialChars.Variable}headers");

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<FormatException>(exception.GetBaseException());
        }

        [Test, Timeout(TestTimeout)]
        public void CorrectHeaderSeparatorTest()
        {
            string url = "http://jsonplaceholder.typicode.com/posts/1";

            ListStructure headers = new ListStructure(new List<Structure>()
            {
                new TextStructure("content-type:json")
            });

            Scripter scripter = new Scripter();
           scripter.InitVariables.Add("method", new TextStructure("put"));
           scripter.InitVariables.Add("url", new TextStructure(url));
           scripter.InitVariables.Add("headers", headers);

            scripter.RunLine($"rest method {SpecialChars.Variable}method url {SpecialChars.Variable}url headers {SpecialChars.Variable}headers");

            Assert.IsTrue(scripter.Variables.GetVariableValue<string>("result").Length > 0);
            Assert.AreEqual("Completed", scripter.Variables.GetVariableValue<string>("status"));
        }

        [Test, Timeout(TestTimeout)]
       // [ExpectedException(typeof(NotSupportedException))]
        public void BadHTTPMethodTest()
        {
            string url = "http://jsonplaceholder.typicode.com/posts/1";

            Scripter scripter = new Scripter();
           scripter.InitVariables.Add("url", new TextStructure(url));
            scripter.Text = ($"rest method {SpecialChars.Text}puttt{SpecialChars.Text} url {SpecialChars.Variable}url");

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<NotSupportedException>(exception.GetBaseException());
        }

        [Test, Timeout(TestTimeout)]
       // [ExpectedException(typeof(UriFormatException))]
        public void BadUrlTest()
        {
            string url = "http/jsonplaceholder.typicode.com/posts";

            Scripter scripter = new Scripter();
           scripter.InitVariables.Add("url", new TextStructure(url));
            scripter.Text = ($"rest method {SpecialChars.Text}post{SpecialChars.Text} url {SpecialChars.Variable}url");

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<UriFormatException>(exception.GetBaseException());
        }
    }
}
