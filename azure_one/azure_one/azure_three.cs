using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Http;

using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Repositories;
using azure_one.Etl.CreateImpTables.Infrastructure;
using azure_one.Etl.Shared.Infrastructure.Db.Contexts;
using azure_one.Etl.Shared.Infrastructure.Global;

namespace azure_three
{
    public class azure_three
    {
        private readonly CreateImpTablesController _createImpTablesController;
        
        public azure_three(
            CreateImpTablesController CreateImpTablesController
        )
        {
            _createImpTablesController = CreateImpTablesController;
        }
        
        [FunctionName("azure_three")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log
        )
        {
            try
            {
                Lg.pr("ETL azure_three started...");                
                Global.ContextId = ContextsEnum.db_test;
                _createImpTablesController.Invoke();
                return new OkObjectResult("Imp tables created");
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
        
    }// class azure_three
    
}// namespace azure_three

