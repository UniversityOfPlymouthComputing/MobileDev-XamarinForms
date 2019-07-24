using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelloBindings;
using FunctionApp;
using HelloBindingsLib;

namespace HelloBindingsTests
{
    [TestClass]
    public class TestLib
    {
        [TestMethod]
        public void TestMethod1()
        {
            PayLoad p1 = new PayLoad
            {
                From = 3,
                Saying = "Hello World"
            };

            string s = p1.ToXML();

            PayLoad p2 = PayLoad.FromXML(s);
            Assert.IsTrue(p2.From == 3, "XML Parsing failed for From field");
            Assert.IsTrue(p2.Saying.Equals("Hello World"), "XML Parsing failed for Saying field");
        }
    }
}
