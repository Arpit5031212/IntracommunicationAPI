using IntraCommunicationWebApi.ViewModels;

namespace IntraCommunicationWebApi.Repositories
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(SignInViewModel user);
    }
}
