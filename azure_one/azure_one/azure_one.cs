using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using azure_one.Etl.RawLoaders.Infrastructure;
using azure_one.Etl.Shared.Infrastructure.Repositories;
using azure_one.Etl.SqlRunners.Infrastructure.Controllers;


//test c# online
//https://techiedelight.com/compiler/
namespace azure_one
{
    public class azure_one
    {
        private readonly LoadStagingDbController _loadStagingDbController;
        private readonly RunSqlFilesController _runSqlFilesController;
        
        public azure_one(
            LoadStagingDbController loadStagingDbController,
            RunSqlFilesController runSqlFilesController
        )
        {
            _loadStagingDbController = loadStagingDbController;
            _runSqlFilesController = runSqlFilesController;
        }
        
        [FunctionName("azure_one")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log
        )
        {
            try
            {
                _loadStagingDbController.Invoke();
                _runSqlFilesController.Invoke();
                log.LogInformation("C# HTTP trigger function processed a request.");

                string name = req.Query["tenant_slug"];

                string responseMessage = string.IsNullOrEmpty(name)
                    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                    : $"Hello, {name}. This HTTP triggered function executed successfully.";

                return new OkObjectResult(responseMessage);
            }
            catch (Exception e)
            {
                ImpErrorsRepository.GetInstance().save("azure_one.task",e.ToString());
                log.LogInformation(e.ToString());
                return new OkObjectResult("wrong");
            }
        } //Task
        
    }// class azure_one
}

