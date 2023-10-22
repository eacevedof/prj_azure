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
         users-search: [GET] http://localhost:7071/api/users-search
        */
        [FunctionName("users-search")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log
        )
        {
            try
            {
                string searchText = req.Query["search"];

                var searchDto = GetUsersBySearchDto.FromPrimitives(searchText);
                var usersListDto = _getUsersService.Invoke(searchDto);

                return new OkObjectResult(usersListDto);
            }
            catch (Exception e)
            {
                log.LogError(e.StackTrace);
                return new OkObjectResult(
                    "Some unexpected error occurred. Please. Contact support if this error continue"
                    )
                    {
                        StatusCode = 500
                    };
            }

        } //async Task
        
    }// class Fn
    
}// namespace Fn

