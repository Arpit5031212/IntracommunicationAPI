using AutoMapper;
using IntraCommunicationWebApi.Model;
using IntraCommunicationWebApi.Repositories;
using IntraCommunicationWebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IntraCommunicationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository user;

        public UserController(IUserRepository user) 
        {
            this.user = user;
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
        public async Task<IActionResult> GetUsersByName([FromQuery] string name)
        {
            var users = await user.GetUsersByName(name);
            if(users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            var _user = await user.GetUserById(id);
            if(user != null)
            {
                return Ok(_user);
            }
            return BadRequest("User Not Found");

        }

        [HttpPatch("patch/{id}")]
        public async Task<IActionResult> UpdateUserPatch([FromRoute] int id, [FromBody] JsonPatchDocument UserPatchDoc)
        {
            if(UserPatchDoc == null)
            {
                return BadRequest("provide details correctly.");
            }
            await user.UpdateUserPatch(id, UserPatchDoc);
            return Ok();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserUpdateViewModel UserUpdate)
        {
            if (UserUpdate == null)
            {
                return BadRequest("provide details correctly.");
            }
            await user.UpdateUser(id, UserUpdate);
            return Ok(user);
        }
    }
}
