using IntraCommunicationWebApi.Models;
using IntraCommunicationWebApi.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntraCommunicationWebApi.Repositories
{
    public interface IGroupRepository
    {
        Task<List<Group>> GetAllGroups();
        Task<List<GroupMember>> GetAllJoinedGroups(int userId);
        Task<Group> GetGroupById(int id);
        Task<Boolean> AddGroupMember(GroupMemberViewModel member);
        Task<Boolean> SendInvite_Request(GroupRequestModel invite);
        Task<Boolean> AcceptInvite(int InviteId);
        Task<List<GroupMember>> GetAllGroupMembers(int groupID);
        Task<List<Group>> GetGroupsByName(string groupName);
        Task<Group> CreateGroup(GroupCreateModel group, int id);
        Task<Group> UpdateGroup(JsonPatchDocument groupPatch, int id);
        Task DeleteGroup(int id);
    }
}
