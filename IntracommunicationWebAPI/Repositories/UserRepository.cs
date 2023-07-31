using AutoMapper;
using IntraCommunicationWebApi.Model;
using IntraCommunicationWebApi.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntraCommunicationWebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly InterCommunicationDBContext dbContext;
        private readonly IMapper mapper;

        public UserRepository(InterCommunicationDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        

        public async Task<List<UserProfile>> GetAllUsers()
        {
            if (dbContext != null)
            {
                var users = await dbContext.UserProfiles.ToListAsync();
                return mapper.Map<List<UserProfile>>(users);
            }

            return null;
        }

        public async Task<List<UserProfile>> GetUsersByName(string name)
        {
            if(dbContext != null)
            {
                var users = await dbContext.UserProfiles.Where(
                    user => user.FirstName == name || 
                    user.LastName == name || 
                    (user.FirstName + " " + user.LastName) == name || 
                    (user.LastName + " " + user.FirstName) == name).ToListAsync();

                return mapper.Map<List<UserProfile>>(users);
            }

            return null;
        }

        public async Task<UserProfile> GetUserById(int userId)
        {
            if (dbContext != null)
            {
                var user =  await dbContext.UserProfiles.FindAsync(userId);
                return user;
            }

            return null;
        }

        public async Task<UserProfile> UpdateUserPatch(int id, JsonPatchDocument userPatch)
        {
            var user = await dbContext.UserProfiles.Where(x => x.UserId == id).FirstOrDefaultAsync();
            if (user != null && userPatch != null)
            {
                userPatch.ApplyTo(user);
                await dbContext.SaveChangesAsync();
                return user;
            }
            return null;

        }

        public async Task<UserProfile> UpdateUser(int id, UserUpdateViewModel userUpdate)
        {
            var user = await dbContext.UserProfiles.Where(x => x.UserId == id).FirstOrDefaultAsync();

            if(user != null && userUpdate != null)
            {
                dbContext.UserProfiles.Update(user);
                await dbContext.SaveChangesAsync();
                return user;
            }
            return null;
        }



    }
}
