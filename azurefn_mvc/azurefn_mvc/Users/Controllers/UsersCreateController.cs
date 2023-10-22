using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Http;

using Fn.Users.Services;

namespace Fn.Users.Controllers
{
    public sealed class UsersCreateController
    {
        private readonly UserCreateService _userCreateService;

        public UsersCreateController(
            UserCreateService userCreateService
        )
        {
            _userCreateService = userCreateService;
        }

        /*
         user-create: [POST] http://localhost:7071/api/user-create
        */
        [FunctionName("user-create")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log
        )
        {
            try
            {
                string fullName = req.Query["fullName"];
                string email = req.Query["email"];

                var userCreateDto = UserCreateDto.FromPrimitives(fullName, email);
                var userCreatedDto = _userCreateService.Invoke(userCreateDto);

                return new OkObjectResult(userCreatedDto);
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

