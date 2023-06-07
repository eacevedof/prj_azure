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
using azure_one.Etl.HaveALook.Application;
using azure_one.Etl.HaveALook.Domain;

namespace azure_have_a_look
{

    public class azure_have_a_look
    {

        private readonly CheckPaginationService _checkPaginationService;
        
        public azure_have_a_look(
            CheckPaginationService checkPaginationService
        )
        {
            _checkPaginationService = checkPaginationService;
        }
        
        [FunctionName("azure_have_a_look")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log
        )
        {
            try
            {
                int page = int.Parse(req.Query["page"]);
                int perPage = int.Parse(req.Query["per_page"]);

                Req.ContextId = ContextsEnum.db_test;
                Req.Request["page"] = page;
                Req.Request["pagesize"] = perPage;
                
                Lg.pr("Azure Have a Look started...");
                
                FilterDto filterDto = FilterDto.fromPrimitives("", page, perPage, "ASC", "id");
                var provincesDto = _checkPaginationService.Invoke(filterDto);
                string jsonString = JsonConvert.SerializeObject(provincesDto);
                
                return new OkObjectResult(provincesDto);
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

