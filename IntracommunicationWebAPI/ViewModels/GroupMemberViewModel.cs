using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IntraCommunicationWebApi.ViewModels
{
    public class GroupMemberViewModel
    {
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int GroupId { get; set; }
    }
}
