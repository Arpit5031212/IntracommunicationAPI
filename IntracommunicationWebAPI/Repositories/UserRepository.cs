using AutoMapper;
using IntraCommunicationWebApi.Models;
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
        private readonly IntraCommunicationDatabaseContext dbContext;
        private readonly IMapper mapper;

        public UserRepository(IntraCommunicationDatabaseContext dbContext, IMapper mapper)
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

        public async Task<UserProfile> UpdateUser(int id, JsonPatchDocument userPatch)
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

        
    }
}
