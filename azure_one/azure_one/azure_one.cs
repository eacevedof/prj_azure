﻿using System;
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
        private readonly RunPostLoadFilesController _runPostLoadFilesController;
        
        public azure_one(
            LoadStagingDbController loadStagingDbController,
            RunPostLoadFilesController runPostLoadFilesController
        )
        {
            _loadStagingDbController = loadStagingDbController;
            _runPostLoadFilesController = runPostLoadFilesController;
        }
        
        [FunctionName("azure_one")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log
        )
        {
            try
            {
                string tenant_slug = req.Query["tenant_slug"];
                string transaction_id = req.Query["transaction_id"];
                
                Lg.pr("ETL azure_one started...");
                _loadStagingDbController.Invoke();
                Lg.pr("ETL staging db loaded!");
                _runPostLoadFilesController.Invoke();
                Lg.pr("ETL sql files executed");

                string successMessage = $"ETL AzureOne ({tenant_slug},{transaction_id}) has finished successfully!.";
                return new OkObjectResult(successMessage);
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

