using BasicNavigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xamarin.Essentials;

namespace PersonDetailsModelTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodSave()
        {
            string mainDir = System.IO.Path.GetTempPath();
            string path = System.IO.Path.Combine(mainDir, "userdetails.xml");
            Console.WriteLine(path);

            PersonDetailsModel model = new PersonDetailsModel();
            model.Save(path);
        }
    }
}
