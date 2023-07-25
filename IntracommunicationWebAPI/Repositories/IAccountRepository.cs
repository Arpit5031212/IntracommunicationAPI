using IntraCommunicationWebApi.Models;
using IntraCommunicationWebApi.ViewModels;
using System.Threading.Tasks;

namespace IntraCommunicationWebApi.Repositories
{
    public interface IAccountRepository
    {
        public Task<UserProfile> SignUp(SignUpRequest user);

        //public Task<UserProfile> SignIn(SignInViewModel user);

        public Task DeleteAccount(int id);
    }
}
