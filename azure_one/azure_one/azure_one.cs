using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Http;

using azure_one.Etl.RawLoaders.Infrastructure;
using azure_one.Etl.Shared.Infrastructure.Log;
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
                Lg.pr("ETL azure_one started...");
                _loadStagingDbController.Invoke();
                Lg.pr("ETL staging db loaded!");
                _runSqlFilesController.Invoke();
                Lg.pr("ETL sql files executed");

                string name = req.Query["tenant_slug"];

                string responseMessage = string.IsNullOrEmpty(name)
                    ? "ETL AzureOne has finished successfully!."
                    : $"Hello, {name}. This HTTP triggered function executed successfully.";

                return new OkObjectResult(responseMessage);
            }
            catch (Exception e)
            {
                log.LogInformation(e.ToString());
                ImpErrorsRepository.add("azure_one.task",e.ToString());
                
                return new OkObjectResult(
                    "Sorry the ETL process failed. Check logs for more information please"
                    )
                    {
                        StatusCode = 500
                    };
            }
        } //Task
        
    }// class azure_one
    
}// namespace azure_one

