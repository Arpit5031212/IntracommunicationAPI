using AutoMapper;
using IntraCommunicationWebApi.Model;
using IntraCommunicationWebApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IntraCommunicationWebApi.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly InterCommunicationDBContext dbContext;

        public AccountRepository(InterCommunicationDBContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        //public async Task<UserProfile> SignIn(SignInViewModel _user)
        //{
        //    var user = await dbContext.UserProfiles.FirstOrDefaultAsync(u => u.Email == _user.Email);

        //    if(user != null && BCrypt.Net.BCrypt.Verify(_user.Password, user.Password)) 
        //    {
        //        return user;
        //    }
        //    return null;
        //}

        public async Task<UserProfile> SignUp(SignUpRequest user)
        {

            string hashedPass = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var new_User = new UserProfile()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Contact = user.Contact,
                Dob = user.Dob,
                Password = hashedPass,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                City = user.City,
                State = user.State,
                PermanentCity = user.PermanentCity,
                PermanentState = user.PermanentState,

            };
            dbContext.UserProfiles.Add(new_User);
            await dbContext.SaveChangesAsync();

            return new_User;
        }

        public async Task DeleteAccount(int id)
        {
            var user = await dbContext.UserProfiles.FindAsync(id);
            if(user != null)
            {
                dbContext.UserProfiles.Remove(user);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
