using System;
using System.Collections.Generic;

namespace AnonymousFunctions
{
    public class ActionClass
    {
        List<int> data = new List<int>()
        {
            1,2,3,4,5,6,7,8,9
        };
        protected void ListOutput(Action<int> classify)
        {
            Console.WriteLine("Outputing List");
            foreach (int n in data) {
                classify(n);
            }
        }
        public ActionClass()
        {
            Console.WriteLine("Using Action<int>");
            ListOutput(u => {
                if ((u % 2) == 0) Console.WriteLine($"{u} is even");
            });

            ListOutput(u => { if (u < 5) Console.WriteLine($"{u} is low"); });
        }
    }
}
