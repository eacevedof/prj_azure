﻿using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using azure_one.Etl.RawLoaders.Infrastructure;

namespace azure_one
{
    public class azure_one
    {
        private readonly RawLoadersController _rawLoadersController;

        public azure_one(RawLoadersController rawLoadersController)
        {
            _rawLoadersController = rawLoadersController;
        }
        
        //https://youtu.be/QWK_XIn9vT4 Como Arrancar con Azure Function
        [FunctionName("azure_one")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log
        ) {
            _rawLoadersController.Invoke();
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["tenant_slug"];

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}

