using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

using azure_one.Etl.Shared.Infrastructure.Global;
using azure_one.Etl.Shared.Infrastructure.Db.Contexts;

using azure_one.Etl.Shared.Infrastructure.Log;

using azure_one.Etl.HaveALook.Domain;
using azure_one.Etl.HaveALook.Infrastructure.Controllers;


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
                string page = req.Query["page"];
                string perPage = req.Query["per_page"];

                Req.ContextId = ContextsEnum.db_test;
                Req.Request["page"] = page;
                Req.Request["pagesize"] = perPage;
                
                Lg.pr("Azure Have a Look started...");
                var provincesDto = _checkPaginationController.Invoke();
                string jsonString = JsonConvert.SerializeObject(provincesDto);
                
                return new OkObjectResult(jsonString);
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

