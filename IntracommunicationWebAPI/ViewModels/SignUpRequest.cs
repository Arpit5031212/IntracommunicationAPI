using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace IntraCommunicationWebApi.ViewModels
{
    public class SignUpRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Contact { get; set; }
        public DateTime Dob { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PermanentCity { get; set; }
        public string PermanentState { get; set; }
    }
}
