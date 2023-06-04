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

//test c# online
//https://techiedelight.com/compiler/
namespace azure_have_a_look
{
    public class azure_have_a_look
    {
        private readonly LoadForceTableController _loadForceTableController;
        
        public azure_have_a_look(
            LoadForceTableController loadForceTableController
        )
        {
            _loadForceTableController = loadForceTableController;
        }
        
        [FunctionName("azure_have_a_look")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log
        )
        {
            try
            {
                string tenant_slug = req.Query["tenant_slug"];
                string transaction_id = req.Query["transaction_id"];
                
                Lg.pr("ETL azure_have_a_look started...");
                _loadForceTableController.Invoke();
                string successMessage = $"ETL AzureTwo ({tenant_slug},{transaction_id}) has finished successfully!.";
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
        
    }// class azure_have_a_look
    
}// namespace azure_have_a_look

