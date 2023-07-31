using IntraCommunicationWebApi.Model;
using IntraCommunicationWebApi.Repositories;
using IntraCommunicationWebApi.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntraCommunicationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository groupRepository;
        public GroupController(IGroupRepository groupRepository) 
        { 
            this.groupRepository = groupRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNewGroup([FromBody] GroupCreateModel group, [FromQuery] int AdminId)
        {
            var new_group = await groupRepository.CreateGroup(group, AdminId);
            if(new_group != null)
            {
                return Ok(new_group);
            }
            return BadRequest("some error occured, try again.");
        }

        [HttpPost("add-member-request")]
        public async Task<IActionResult> AddGroupMembers([FromBody] GroupMemberViewModel member)
        {
            if (member == null) return BadRequest();
            var isAdded = await groupRepository.AddGroupMember(member);
            if(isAdded == true)
            {
                return Ok(new { message = "member added" });
            }else
            {
                return Ok(new { message = "invite sent" });
            }
        }

        [HttpGet("invites")]
        public async Task<IActionResult> GetAllInvites([FromQuery] int userId)
        {
            var invites = await groupRepository.GetAllInvites(userId); 
            return Ok(invites);
        }

        [HttpPost("accept-invitation")]
        public async Task<IActionResult> AcceptInvite([FromQuery] int inviteId)
        {
            var accepted = await groupRepository.AcceptInvite(inviteId);
            if(accepted)
            {
                return Ok(new { message = "user accepted invitation" });
            }else
            {
                return Ok(new { message = "user rejected invitation" });
            }
        }
         
        [HttpGet("members/{groupID}")]
        public async Task<IActionResult> GetAllGroupMembers([FromRoute] int groupID)
        {
            var members = await groupRepository.GetAllGroupMembers(groupID);
            if (members == null)
            {
                return NotFound();
            }
            return Ok(members);
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await groupRepository.GetAllGroups();
            if (groups == null)
            {
                return NotFound();
            }
            return Ok(groups);
        }

        [HttpGet("joined")]
        public async Task<IActionResult> GetAllJoinedGroups([FromQuery] int userId)
        {
            var groups = await groupRepository.GetAllJoinedGroups(userId);
            if (groups == null)
            {
                return NotFound();
            }
            return Ok(groups);
        }


        [HttpGet("searchByName")]
        public async Task<IActionResult> GetGroupsByName([FromQuery] string groupName)
        {
            var groups = await groupRepository.GetGroupsByName(groupName);
            if (groups == null)
            {
                return NotFound();
            }
            return Ok(groups);
        }

        [HttpGet("searchById")]
        public async Task<IActionResult> GetGroupById([FromQuery] int groupId)
        {
            var groups = await groupRepository.GetGroupById(groupId);
            if (groups == null)
            {
                return NotFound();
            }
            return Ok(groups);
        }



        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateGroup([FromBody] JsonPatchDocument groupPatch, [FromRoute] int id)
        {
            var group = await groupRepository.UpdateGroup(groupPatch, id);
            if(group == null)
            {
                return BadRequest();
            }
            return Ok(group);
        }

    }
}
