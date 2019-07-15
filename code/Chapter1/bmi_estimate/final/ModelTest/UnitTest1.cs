using Microsoft.VisualStudio.TestTools.UnitTesting;
using bmi_estimate;

namespace ModelTest
{
    [TestClass]
    public class Modeltests
    {
        [TestMethod]
        public void TestBuildUp()
        {
            var m = new BmiModel();
            string errString;

            bool valid1 = m.SetHeightAsString("2.0", out errString);
            Assert.IsTrue(valid1);
            double? v = m;
            Assert.IsTrue(v == null, "BMIs should be null at this point");
            Assert.IsTrue(errString.Equals(""));

            bool valid2 = m.SetWeightAsString("100.0", out errString);
            Assert.IsTrue(valid2);
            Assert.IsTrue(m == 25.0, "BMI should be 25");
            Assert.IsTrue(errString.Equals(""));

        }

        [TestMethod]
        public void TestInvalidate()
        {
            var m = new BmiModel();
            string errString;

            m.SetHeightAsString("2.0", out errString);
            m.SetWeightAsString("100.0", out errString);
            bool valid = m.SetHeightAsString("0.1", out errString);
            Assert.IsFalse(valid);
            double? v = m;
            Assert.IsTrue(v == null, "BMIs should be null at this point");
            Assert.IsTrue(errString.Equals("Height must be between 0.5 and 3.0m"));
        }


        [TestMethod]
        public void TestInvalidString()
        {
            var m = new BmiModel();
            string errString;

            m.SetHeightAsString("2.0", out errString);
            m.SetWeightAsString("100.0", out errString);
            bool valid = m.SetHeightAsString("0.5a", out errString);
            Assert.IsFalse(valid);
            double? v = m;
            Assert.IsTrue(v == null, "BMIs should be null at this point");
            Assert.IsTrue(errString.Equals("Please enter a numerical value"));
        }
    }

    [TestClass]
    public class BodyParameterTests
    {
        [TestMethod]
        public void TestLowerEdge()
        {
            var p1 = new BodyParameter(min: 20.0, max: 200.0, "Weight", "Kg");

            string errStr;
            bool res1 = p1.SetValueFromString("20.0", out errStr);

            Assert.IsTrue(res1, "SetValueFromString failed lower edge case");
            Assert.IsTrue(p1 == 20.0, "SetValueFromString had wrong value for lower edge case");
            Assert.IsTrue(errStr.Equals(""), "Error string incorrect for lower edge");
        }

        [TestMethod]
        public void TestUpperEdge()
        {
            var p1 = new BodyParameter(min: 20.0, max: 200.0, "Weight", "Kg");

            string errStr;
            bool res1 = p1.SetValueFromString("200.0", out errStr);

            Assert.IsTrue(res1, "SetValueFromString failed upper edge case");
            Assert.IsTrue(p1 == 200.0, "SetValueFromString had wrong value for lower edge case");
            Assert.IsTrue(errStr.Equals(""), "Error string incorrect for upper edge");

        }

        [TestMethod]
        public void TestBelowLowerEdge()
        {
            var p1 = new BodyParameter(min: 20.0, max: 200.0, "Weight", "Kg");

            string errStr;
            bool res1 = p1.SetValueFromString("19.99", out errStr);
            double? v = p1;
            Assert.IsNull(v, "Out of range value did not return null");
            Assert.IsFalse(res1, "SetValueFromString failed for value below lower edge");
            Assert.IsTrue(errStr.Equals("Weight must be between 20.0 and 200.0Kg"), "Error string incorrect for below lower edge");

        }
        [TestMethod]
        public void TestAboveUpperEdge()
        {
            var p1 = new BodyParameter(min: 20.0, max: 200.0, "Weight", "Kg");

            string errStr;
            bool res1 = p1.SetValueFromString("200.001", out errStr);
            double? v = p1;
            Assert.IsNull(v, "Out of range value did not return null");
            Assert.IsFalse(res1, "SetValueFromString failed for value above upper edge");
            Assert.IsTrue(errStr.Equals("Weight must be between 20.0 and 200.0Kg"), "Error string incorrect for above upper edge");

        }
        [TestMethod]
        public void TestNullString()
        {
            var p1 = new BodyParameter(min: 20.0, max: 200.0, "Weight", "Kg");
            string errStr;
            bool res1 = p1.SetValueFromString("", out errStr);
            double? v = p1;
            Assert.IsNull(v, "Empty string value did not return null");
            Assert.IsFalse(res1, "Failed to detect empty string");
            Assert.IsTrue(errStr.Equals("Please enter a numerical value"), "Error string \"" + errStr + "\" is incorrect for null string:");
        }

        [TestMethod]
        public void TestInvalidString()
        {
            var p1 = new BodyParameter(min: 20.0, max: 200.0, "Weight", "Kg");
            string errStr;
            bool res1 = p1.SetValueFromString("12a", out errStr);
            double? v = p1;
            Assert.IsNull(v, "Invalid string did not return null");
            Assert.IsFalse(res1, "Failed to detect invalid string");
            Assert.IsTrue(errStr.Equals("Please enter a numerical value"), "Error string incorrect for invalid string input");
        }
    }
}
