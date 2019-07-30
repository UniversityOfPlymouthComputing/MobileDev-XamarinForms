using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelloBindings;
using HelloBindingsLib;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HelloBindingsTests
{

    [TestClass]
    public class TestVM : IMainPageViewHelper, ICommand
    {
        bool ButtonCanExecute { get; set; } = false;
        Action ButtonExecuteAction { get; set; }
        Func<bool> ButtonCanExecuteFunction { get; set; }

        bool canChangeExecuteInvoked = false;

        public event EventHandler CanExecuteChanged;

        public void ChangeCanExecute(ICommand obj)
        {
            canChangeExecuteInvoked = true;
            ButtonCanExecute = ButtonCanExecuteFunction();
        }

        public ICommand CreateConcreteCommand(Action execute, Func<bool> canExecute)
        {
            ButtonExecuteAction = execute;
            ButtonCanExecuteFunction = canExecute;
            return this;
        }

        public Task ShowErrorMessageAsync(string ErrorMessage)
        {
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        public Task ShowModalAboutPageAsync()
        {
            Console.WriteLine("ModalAbout");
            return Task.CompletedTask;
        }

        [TestMethod]
        public void TestMethod1()
        {
            SayingsAbstractModel m = new MockedRemoteModel();
            MainPageViewModel vm = new MainPageViewModel(m, this);
            bool ce = vm.FetchNextSayingCommand.CanExecute(null);
            Assert.IsTrue(ce, "Command Cannot Execute at Startup");
            vm.FetchNextSayingCommand.Execute(null);
            Assert.IsTrue(vm.CurrentSaying.Equals("May the Force be With You"), "Wrong sayng: " + vm.CurrentSaying);
        }

        public bool CanExecute(object parameter)
        {
            return ButtonCanExecuteFunction();
        }

        public void Execute(object parameter)
        {
            ButtonExecuteAction();
        }
    }
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

    [TestClass] 
    public class TestRemoteModel
    {
        private static List<string> Sayings = new List<string>
        {
            "May the Force be With You",
            "Live long and prosper",
            "Nanoo nanoo",
            "Make it So!"
        };

        [TestMethod]
        public async System.Threading.Tasks.Task TestFetchAsync()
        {

            SayingsAbstractModel m = new MockedRemoteModel();

            //Now how the following "captures m"
            Func<Task> atest = async () =>
            {
                (bool success, string ErrorString) = await m.NextSaying();
                Assert.IsTrue(success, "NextSaying failed");
                Assert.IsTrue(ErrorString.Equals("OK"));
                Assert.IsTrue(m.CurrentSaying.Equals(Sayings[0]));
                (success, ErrorString) = await m.NextSaying();
                Assert.IsTrue(success, "NextSaying failed");
                Assert.IsTrue(ErrorString.Equals("OK"));
                Assert.IsTrue(m.CurrentSaying.Equals(Sayings[1]));
            };

            await atest();

            //Change m
            m = new RemoteModel();

            //Rerun tests
            await atest();
        }
    }
}
