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
            Scripter scripter = new Scripter();
            string url = "https://httpbin.org/post";

            ListStructure list = new ListStructure(new List<object>()
            {
                "something:val"
            },null,scripter);

           
           scripter.InitVariables.Clear();
           scripter.InitVariables.Add("url", new TextStructure(url));
           scripter.InitVariables.Add("method", new TextStructure("post"));
           scripter.InitVariables.Add("list", list);

            scripter.Text = ($@"rest method {SpecialChars.Variable}method url {SpecialChars.Variable}url parameters {SpecialChars.Variable}list timeout {TestTimeout}
                               {SpecialChars.Variable}result2 = {SpecialChars.Variable}result{SpecialChars.IndexBegin}['form']['something']{SpecialChars.IndexEnd}");
            scripter.Run();

            Assert.AreEqual("val", scripter.Variables.GetVariableValue<string>("result2"));
            Assert.AreEqual("Completed", scripter.Variables.GetVariableValue<string>("status"));
        }

        [Test, Timeout(TestTimeout)]
        public void SendDataWithHeadersTest()
        {
            string url = "https://httpbin.org/put";
            string url2 = "https://httpbin.org/get";
            ListStructure parameters = new ListStructure(new List<object>()
            {
                "something:val"
            });

            ListStructure headers1 = new ListStructure(new List<object>()
            {
               "encoding-language:foo",
                "version:3.0"
            });
            ListStructure headers2 = new ListStructure(new List<object>()
            {
                "type1:xml"
            });

            Scripter scripter = new Scripter();
            scripter.InitVariables.Clear();
            scripter.InitVariables.Add("params", parameters);
            scripter.InitVariables.Add("url", new TextStructure(url));
            scripter.InitVariables.Add("method", new TextStructure("put"));
            scripter.InitVariables.Add("headers", headers1);
            scripter.InitVariables.Add("url2", new Language.TextStructure(url2));
            scripter.InitVariables.Add("method2", new Language.TextStructure("get"));
            scripter.InitVariables.Add("headers2", headers2);

            scripter.Text = ($@"rest method {SpecialChars.Variable}method url {SpecialChars.Variable}url headers {SpecialChars.Variable}headers parameters {SpecialChars.Variable}params timeout {TestTimeout} result {SpecialChars.Variable}result1
                                json {SpecialChars.Variable}result jpath ['headers']['Encoding-Language'] result {SpecialChars.Variable}json1
                                json {SpecialChars.Variable}result jpath ['headers']['Version'] result {SpecialChars.Variable}json2
                                rest method {SpecialChars.Variable}method2 url {SpecialChars.Variable}url2 headers {SpecialChars.Variable}headers2 parameters {SpecialChars.Variable}params result {SpecialChars.Variable}result12
                                json {SpecialChars.Variable}result jpath ['headers']['Type1'] result {SpecialChars.Variable}ress");
            scripter.Run();

            Assert.AreEqual("xml", scripter.Variables.GetVariableValue<string>("ress"));

            string resultJson = scripter.Variables.GetVariableValue<string>("result1");
            resultJson = scripter.Variables.GetVariableValue<string>("result12");
            Assert.AreEqual("3.0", scripter.Variables.GetVariableValue<string>("json2"));
            Assert.AreEqual("foo", scripter.Variables.GetVariableValue<string>("json1"));
        }

        [Test, Timeout(TestTimeout)]
        //[ExpectedException(typeof(TimeoutException))]
        public void TimeoutTest()
        {
            string url = "http://validate.jsontest.com/";

            Scripter scripter = new Scripter();
            scripter.InitVariables.Clear();
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
scripter.InitVariables.Clear();
           scripter.InitVariables.Add("method", new Language.TextStructure("delete"));
           scripter.InitVariables.Add("url", new Language.TextStructure(url));
            scripter.Text =($"rest method {SpecialChars.Variable}method url {SpecialChars.Variable}url");
            scripter.Run();

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
scripter.InitVariables.Clear();
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
scripter.InitVariables.Clear();
           scripter.InitVariables.Add("method", new TextStructure("put"));
           scripter.InitVariables.Add("url", new TextStructure(url));
           scripter.InitVariables.Add("headers", headers);

            scripter.Text =($"rest method {SpecialChars.Variable}method url {SpecialChars.Variable}url headers {SpecialChars.Variable}headers");
            scripter.Run();

            Assert.IsTrue(scripter.Variables.GetVariableValue<string>("result").Length > 0);
            Assert.AreEqual("Completed", scripter.Variables.GetVariableValue<string>("status"));
        }

        [Test, Timeout(TestTimeout)]
       // [ExpectedException(typeof(NotSupportedException))]
        public void BadHTTPMethodTest()
        {
            string url = "http://jsonplaceholder.typicode.com/posts/1";

            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
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
scripter.InitVariables.Clear();
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
