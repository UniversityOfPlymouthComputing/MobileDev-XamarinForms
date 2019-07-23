using System;
using System.Collections.Generic;
using HelloBindingsLib;

namespace FunctionApp
{
    public class RemoteModel
    {
        public static List<string> Sayings = new List<string>
        {
            "May the Force be With You",
            "Live long and prosper",
            "Nanoo nanoo"
        };
        public static int Count => RemoteModel.Sayings.Count;

        public static string Fetch(string StringIndex, out string ErrorString)
        {
            bool success = int.TryParse(StringIndex, out int index);
            if (success)
            {
                if ((index >= 0) && (index < RemoteModel.Count))
                {
                    ErrorString = null;
                    PayLoad p = new PayLoad
                    {
                        Saying = Sayings[index],
                        Index = index,
                        From = Count
                    };
                    return p.ToXML();
                }
                else
                {
                    ErrorString = "Index out of range";
                    return null;
                }
            }
            else
            {
                ErrorString = "Please pass a index on the query string as an integer";
                return null;
            }

        }

        static RemoteModel Create()
        {
            return new RemoteModel();
        }

        public RemoteModel()
        {

        }


    }
}
