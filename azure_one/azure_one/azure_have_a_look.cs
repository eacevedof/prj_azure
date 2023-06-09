using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Http;

using azure_one.Etl.Shared.Infrastructure.Global;
using azure_one.Etl.Shared.Infrastructure.Db.Contexts;

using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.HaveALook.Application;
using azure_one.Etl.HaveALook.Domain;
using azure_one.Etl.HaveALook.Domain.Exceptions;
using azure_one.Etl.HaveALook.Domain.Rules;

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
                Lg.pr("Azure Have a Look started...");
                RequestValidator requestValidator = new();

                IQueryCollection httpQuery = req.Query;
                requestValidator.Invoke(httpQuery);

                int page = int.Parse(httpQuery["page"]);
                int perPage = int.Parse(httpQuery["per_page"]);
                string search = httpQuery.ContainsKey("search") ? httpQuery["search"] : "";
                string orderBy = httpQuery.ContainsKey("order_by") ? httpQuery["order_by"] : "DESC";
                string orderColumn = httpQuery.ContainsKey("order_column") ? httpQuery["order_column"] : "id";

                Req.ContextId = ContextsEnum.db_test;                
                                
                FilterDto filterDto = FilterDto.fromPrimitives(search, page, perPage, orderBy, orderColumn);
                ProvincesDto provincesDto = _checkPaginationService.Invoke(filterDto);                
                return new OkObjectResult(provincesDto);
            }
            catch (AbstractHaveALookException e)
            {
                return new OkObjectResult(e.Message) { StatusCode = e.GetCode()};
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

