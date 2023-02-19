using System.IO;
using System.Threading.Tasks;
using azure_one.Etl.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using azure_one.Etl.Infrastructure.Log;

namespace azure_one
{
    public class azure_one
    {
        private readonly CreateUserService _createUserService;

        public azure_one(CreateUserService createUserService)
        {
            _createUserService = createUserService;
        }
        
        //https://youtu.be/QWK_XIn9vT4 Como Arrancar con Azure Function
        [FunctionName("azure_one")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log
        ) {
            Lg.Pr("empieza azure-one...");
            this._createUserService.InsertRandomUser();
            this._createUserService.PrintAll();
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}

