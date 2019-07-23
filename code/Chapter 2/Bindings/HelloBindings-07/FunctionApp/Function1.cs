using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FunctionApp
{
    public static class Function1
    {
        private static List<string> Sayings = new List<string>
        {
            "May the Force be With You",
            "Live long and prosper",
            "Nanoo nanoo"
        };
        private static int Count => Function1.Sayings.Count;

        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "LookupSaying")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["index"];
            bool success = int.TryParse(name, out int index);
            if (success)
            {
                if ((index >= 0) && (index < Function1.Count))
                {
                    return (ActionResult)new OkObjectResult(Function1.Sayings[index]);
                }
                else
                {
                    return new BadRequestObjectResult("Index out of range");
                }

            } else
            {
                 return new BadRequestObjectResult("Please pass a index on the query string as an integer");
            }

        }
    }
}
