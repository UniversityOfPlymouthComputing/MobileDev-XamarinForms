using MyLibrary;

namespace LC
{
    public class MyClass //: IListener
    {
        LibClass mylib = new LibClass();
        public MyClass()
        {

            //Hook up the call-back
            //mylib.Originator = this;

            //Call library method
            mylib.Shout("Over hear!");

        }

        /*
        //Call back
        public void ShoutBack(string msg)
        {
            System.Console.WriteLine($"Originator says {msg}");
        }
        */
    }
}
