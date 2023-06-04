using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Http;

using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.HaveALook.Infrastructure;

namespace azure_have_a_look
{
    public class azure_have_a_look
    {
        private readonly CheckPaginationController _checkPaginationController;
        
        public azure_have_a_look(
            CheckPaginationController checkPaginationController
        )
        {
            _checkPaginationController = checkPaginationController;
        }
        
        [FunctionName("azure_have_a_look")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log
        )
        {
            try
            {
                string table = req.Query["table"];
                string perPage = req.Query["per_page"];
                
                Lg.pr("ETL azure_have_a_look started...");
                _checkPaginationController.Invoke();
                string successMessage = $"AzureHaveALook ({table},{perPage}) has finished successfully!.";
                return new OkObjectResult(successMessage);
            }
            catch (Exception e)
            {
                log.LogInformation(e.ToString());
                return new OkObjectResult(
                    "Sorry Have a Look Pagination failed. Check logs for more information please"
                    )
                    {
                        StatusCode = 500
                    };
            }
        } //Task
        
    }// class azure_have_a_look
    
}// namespace azure_have_a_look

