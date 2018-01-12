

using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;

namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExcelSetValTests
    {
        static int intVal = 5;
        static float fVal = 3.3f;
        static string stringVal = "something";
        static string formula = "=B1*C1";
        static Scripter scripter;

        private void KillProcesses()
        {
            foreach (Process p in Process.GetProcessesByName("excel"))
            {
                try
                {
                    p.Kill();
                }
                catch { }
            }
        }

        [OneTimeSetUp]
        public static void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            scripter = new Scripter();
scripter.InitVariables.Clear();
           scripter.InitVariables.Add("intVal", new Language.IntegerStructure(intVal));
           scripter.InitVariables.Add("fVal", new FloatStructure(fVal));
           scripter.InitVariables.Add("strVal", new TextStructure(stringVal));
           scripter.InitVariables.Add("formula", new TextStructure(formula));
        }
        

        [SetUp]
        public void TestInit()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.MSOffice.dll");
            scripter.RunLine($"excel.open");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelSetValTest()
        {
            scripter.RunLine($"excel.setvalue {SpecialChars.Variable}strVal row 1 colindex 1");
            scripter.RunLine($"excel.setvalue {SpecialChars.Variable}intVal row 1 colindex 2");
            scripter.RunLine($"excel.setvalue {SpecialChars.Variable}fVal row 1 colindex 3");
            scripter.RunLine($"excel.setvalue {SpecialChars.Variable}formula row 1 colindex 4");

            scripter.RunLine("excel.getvalue row 1 colindex 1");
            Assert.AreEqual(stringVal, scripter.Variables.GetVariableValue<string>("result"));
            scripter.RunLine("excel.getvalue row 1 colindex 2");
            Assert.AreEqual(intVal, Int32.Parse(scripter.Variables.GetVariable("result").GetValue().Object as String));
            scripter.RunLine("excel.getvalue row 1 colindex 3");
            Assert.AreEqual(fVal, float.Parse((scripter.Variables.GetVariable("result").GetValue().Object as String).Replace(",", ".")));
            scripter.RunLine($"excel.getvalue row 1 colindex 4 result {SpecialChars.Variable}product");
            Assert.AreEqual(intVal * fVal, float.Parse(scripter.Variables.GetVariableValue<string>("product").Replace(",", ".")), 0.000001);
        }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.RunLine("excel.close");
            Process[] proc = Process.GetProcessesByName("excel");
            if (proc.Length != 0)
            {
                KillProcesses();
            }
        }
    }
}
