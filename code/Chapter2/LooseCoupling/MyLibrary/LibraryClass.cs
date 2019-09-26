using System;

namespace MyLibrary
{

    public class LibaryClass
    {
        protected IMessageLogger Logger { get; set; }

        //Note the type of the parameter is an interface, NOT a concrete class
        //This means ANY class conforming to this interface can be use this library
        //We call this loose coupling
        public LibaryClass(IMessageLogger logger)
        {
            Logger = logger;
        }

        //This is one of the library methods that is available
        public void DoUsefulThing()
        {
            //Do something 
            Console.WriteLine($"{GetType().Assembly.GetName().Name}: Useful Library Function invoked");

            //Call back the object that instantiated this
            Logger.LogMessage("The library function logs a message");

            //Call completion Handler
            Logger.Complete(true);
        }

    }

}
