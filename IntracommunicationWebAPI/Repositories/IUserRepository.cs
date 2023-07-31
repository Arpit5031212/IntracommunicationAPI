using IntraCommunicationWebApi.Model;
using IntraCommunicationWebApi.ViewModels;
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
        Task<UserProfile> UpdateUserPatch(int id, JsonPatchDocument user);
        Task<UserProfile> UpdateUser(int id, UserUpdateViewModel user);
        Task<UserProfile> GetUserById(int userId);
    }
}
