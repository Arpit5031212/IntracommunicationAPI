using AutoMapper;
using IntraCommunicationWebApi.Models;
using IntraCommunicationWebApi.Repositories;
using IntraCommunicationWebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace IntraCommunicationWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAccountRepository account;
        private readonly IJWTManagerRepository jwtManager;
        private readonly IntraCommunicationDatabaseContext dbContext;
        public AccountController(IAccountRepository account, IMapper mapper, IntraCommunicationDatabaseContext dbContext, IJWTManagerRepository jwtManager)
        {
            this.account = account;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.jwtManager = jwtManager;
        }
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest user)
        {
            if (user == null)
            {
                return BadRequest("user is invalid");
            }
            if(dbContext.UserProfiles.Any(u => u.Email == user.Email))
            {
                return BadRequest("User already Exist");
            }

            var new_user = await account.SignUp(user);
            return Ok(new_user);
        }

        //[HttpPost("signin")]
        //public async Task<IActionResult> signIn(SignInViewModel signInRequest)
        //{
        //    var user = await account.SignIn(signInRequest);

        //    if(user == null)
        //    {
        //        return BadRequest("user not found. check email and password.");
        //    }
        //    return Ok(user);
        //}

        [AllowAnonymous]
        [HttpPost("signIn")]
        public IActionResult Authenticate([FromBody] SignInViewModel user)
        {
            var _user = dbContext.UserProfiles.Where(u => u.Email == user.Email).FirstOrDefault();
            var token = jwtManager.Authenticate(user);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token, id = _user.UserId, message = "Login Successful" });
        }

        [HttpDelete("delete-account/{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id)
        {
            if(id != 0)
            {
                await account.DeleteAccount(id);
                return Ok("account deleted");
            }

            return BadRequest();
        }
    }
}
