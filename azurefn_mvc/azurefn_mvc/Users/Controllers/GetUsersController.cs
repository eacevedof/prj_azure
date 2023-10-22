using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Http;


using Fn.Users.Services;


//resources
//https://techiedelight.com/compiler/
//https://dotnetfiddle.net/

namespace Fn.Users.Controllers
{
    public sealed class GetUsersController
    {
        private readonly GetUsersService _getUsersService;

        public GetUsersController(
            GetUsersService getUsersService
        )
        {
            _getUsersService = getUsersService;
        }

        /*
         users-index: [GET,POST] http://localhost:7071/api/users-index
        */
        [FunctionName("users-index")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log
        )
        {
            try
            {
       
                string tenant_slug = req.Query["tenant_slug"];



                string successMessage = $"ETL AzureOne ({tenant_slug}) has finished successfully!.";
                return new OkObjectResult(successMessage);
            }
            catch (Exception e)
            {
                return new OkObjectResult(
                    "Sorry the ETL process failed. Check logs for more information please"
                    )
                    {
                        StatusCode = 500
                    };
            }

        } //async Task
        
    }// class Fn
    
}// namespace Fn

