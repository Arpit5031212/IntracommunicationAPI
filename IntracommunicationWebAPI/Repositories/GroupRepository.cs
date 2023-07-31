using AutoMapper;
using IntraCommunicationWebApi.Model;
using IntraCommunicationWebApi.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntraCommunicationWebApi.Repositories
{
    public class GroupRepository : IGroupRepository
    {

        private readonly InterCommunicationDBContext dbContext;
        private readonly IMapper mapper;
        
        public GroupRepository(InterCommunicationDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<GroupInvitesRequest>> GetAllInvites(int userId)
        {
            var invite = await dbContext.GroupInvitesRequests.Where(i => i.SentTo == userId).ToListAsync();
            return invite;
        }

        public async Task<Boolean> SendInvite_Request(GroupRequestModel invite)
        {
            if (invite == null) return false;
            var new_invite = new GroupInvitesRequest()
            {
                SentTo = invite.SentTo,
                GroupId = invite.GroupId,
                IsAccepted = invite.IsAccepted,
                IsApproved = invite.IsApproved,
                CreatedBy = invite.CreatedBy,
                CreatedAt = invite.CreatedAt,
                UpdatedAt = invite.UpdatedAt,
            };
            await dbContext.GroupInvitesRequests.AddAsync(new_invite);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Boolean> AddGroupMember(GroupMemberViewModel member)
        {
            var new_member = new GroupMember()
            {
                GroupId = member.GroupId,
                MemberId = member.MemberId,
            };
            // Check if a user has already sent request to join the group
            var isRequested = await dbContext.GroupInvitesRequests.Where(gID => (gID.GroupId == member.GroupId) && (gID.CreatedBy == member.MemberId)).FirstOrDefaultAsync();

            // the user has not sent request then invite the user
            if (isRequested == null)
            {
                var group = await dbContext.Groups.Where(g => g.GroupId == member.GroupId).FirstOrDefaultAsync();

                var invite = new GroupRequestModel()
                {
                    SentTo = member.MemberId,
                    GroupId = member.GroupId,
                    IsAccepted = false,
                    IsApproved = true,
                    CreatedBy = group.CreatedBy,
                    CreatedAt= System.DateTime.Now,
                    UpdatedAt = System.DateTime.Now,
                };
                return await SendInvite_Request(invite);

            }else
            {
                // if the user has sent the request to join then approve his request.
                isRequested.IsApproved = true;
                await dbContext.GroupMembers.AddAsync(new_member);
                return true;
            }
        }

        public async Task<Boolean> AcceptInvite(int inviteID)
        {
            var invite = await dbContext.GroupInvitesRequests.FindAsync(inviteID);

            if(invite != null)
            {
                invite.IsAccepted = true;
                var member = new GroupMember()
                {
                    MemberId = invite.SentTo,
                    GroupId = invite.GroupId,
                };
                await dbContext.GroupMembers.AddAsync(member);
                dbContext.GroupInvitesRequests.Remove(invite);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
            
        }



        public async Task<List<GroupMember>> GetAllGroupMembers(int groupID)
        {
            if (dbContext != null)
            {
                var members = await dbContext.GroupMembers.Where(member => member.GroupId == groupID).ToListAsync();
                return mapper.Map<List<GroupMember>>(members);
            }
            return null;
        }

        public async Task<List<Group>> GetAllGroups()
        {

            if (dbContext != null)
            {
                var groups = await dbContext.Groups.ToListAsync();
                return mapper.Map<List<Group>>(groups);
            }
            return null;
        }

        public async Task<List<GroupMember>> GetAllJoinedGroups(int userId)
        {

            if (dbContext != null)
            {
                var groups = await dbContext.GroupMembers.Where(g => g.MemberId == userId).ToListAsync();
                return mapper.Map<List<GroupMember>>(groups);
            }
            return null;
        }

        public async Task<Group> GetGroupById(int groupId) 
        {
            if (dbContext != null)
            {
                var group = await dbContext.Groups.Where(g => g.GroupId == groupId).FirstOrDefaultAsync();
                return group;
            }
            return null;
        }



        public async Task<List<Group>> GetGroupsByName(string groupName)
        {
            if (dbContext != null)
            {
                var groups = await dbContext.Groups.Where(group => group.GroupName == groupName).ToListAsync();
                return mapper.Map<List<Group>>(groups).ToList();
            }
            return null;
        }

        public async Task<Group> CreateGroup(GroupCreateModel group, int AdminId)
        {

            if (group != null)
            {
                var new_group = new Group()
                {
                    GroupName = group.GroupName,
                    GroupDescription = group.Description,
                    GroupType = group.GroupType,
                    CreatedAt = System.DateTime.Now,
                    CreatedBy = AdminId,
                };

                try
                {
                await dbContext.Groups.AddAsync(new_group);
                await dbContext.SaveChangesAsync();
                }catch(System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

                //new_group.GroupId

                var admin = new GroupMember() { MemberId = AdminId, GroupId = new_group.GroupId };
                dbContext.GroupMembers.Add(admin);
                await dbContext.SaveChangesAsync();
                return new_group;
            }
            return null;
        }

        public async Task<Group> UpdateGroup(JsonPatchDocument groupPatch, int id) 
        {
            var groupData = await dbContext.Groups.FindAsync(id);
            if(groupData != null && groupPatch != null)
            {
                groupPatch.ApplyTo(groupData);
                await dbContext.SaveChangesAsync();
                return groupData;
            }
            return null;
        }

        public async Task DeleteGroup(int id)
        {

            var group = await dbContext.Groups.FindAsync(id);
            if(group != null )
            {
                dbContext.Groups.Remove(group);
                await dbContext.SaveChangesAsync();
            }
        }


    }
}
