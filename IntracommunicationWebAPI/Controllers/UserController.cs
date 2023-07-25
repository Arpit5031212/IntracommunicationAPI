using AutoMapper;
using IntraCommunicationWebApi.Models;
using IntraCommunicationWebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IntraCommunicationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserRepository user;

        public UserController(IUserRepository user, IMapper mapper) 
        {
            this.user = user;
            this.mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await user.GetAllUsers();
            if(users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        

        [HttpGet("name")]
        public async Task<IActionResult> GetUsersByName([FromBody] string name)
        {
            var users = await user.GetUsersByName(name);
            if(users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] JsonPatchDocument UserPatchDoc)
        {
            if(UserPatchDoc == null)
            {
                return BadRequest("provide details correctly.");
            }
            await user.UpdateUser(id, UserPatchDoc);
            return Ok();
        }
    }
}
