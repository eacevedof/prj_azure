using System;
using System.IO;
using Newtonsoft.Json;
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
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic request = JsonConvert.DeserializeObject(requestBody);

                string fullName = request?.fullName;
                fullName = fullName.Trim();

                string email = request?.email;
                email = email.Trim();

                if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email))
                    return new OkObjectResult("Missing fullName or email")
                    {
                        StatusCode = 400
                    };

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

        } //method Run
        
    }// class UsersCreateController
    
}// namespace Fn.Users.Controllers

