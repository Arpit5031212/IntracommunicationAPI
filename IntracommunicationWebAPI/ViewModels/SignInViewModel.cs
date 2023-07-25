using System.ComponentModel.DataAnnotations;

namespace IntraCommunicationWebApi.ViewModels
{
    public class SignInViewModel
    {
        [Required, DataType(DataType.EmailAddress)] 
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
