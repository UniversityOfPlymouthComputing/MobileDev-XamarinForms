using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using HelloBindingsLib;

namespace FunctionApp
{
    public static class Function1
    {
        private static List<string> Sayings = new List<string>
        {
            "May the Force be With You",
            "Live long and prosper",
            "Nanoo nanoo",
            "Make it So!"
        };
        private static int Count => Sayings.Count;

        [FunctionName("Function1")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "LookupSaying")] HttpRequest req)
        {
            string name = req.Query["index"];
            bool success = int.TryParse(name, out int index);
            if (success)
            {
                if ((index >= 0) && (index < Count))
                {
                    PayLoad p = new PayLoad
                    {
                        From = Count,
                        Saying = Sayings[index]
                    };
                    return new OkObjectResult(p.ToXML());
                }
                else
                {
                    return new BadRequestObjectResult("Index out of range. Please use an index from 0.." + (Count - 1));
                }

            }
            else
            {
                return new BadRequestObjectResult("The query string parameter index must be an integer");
            }

        }
    }
}
