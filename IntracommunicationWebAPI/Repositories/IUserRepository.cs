using IntraCommunicationWebApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntraCommunicationWebApi.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserProfile>> GetAllUsers();
        Task<List<UserProfile>> GetUsersByName(string name);
        Task<UserProfile> UpdateUser(int id, JsonPatchDocument user);
    }
}
