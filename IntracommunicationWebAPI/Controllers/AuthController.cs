using IntraCommunicationWebApi.Repositories;
using IntraCommunicationWebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntraCommunicationWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTManagerRepository jwtManager;

        public AuthController(IJWTManagerRepository jwtManager) 
        {
            this.jwtManager = jwtManager;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] SignInViewModel user) 
        {
            var token = jwtManager.Authenticate(user);

            if(token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
