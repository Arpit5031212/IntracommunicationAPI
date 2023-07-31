using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace IntraCommunicationWebApi.ViewModels
{
    public class GroupCreateModel
    {
        [Required, StringLength(50)]
        public string GroupName { get; set; }
        public string Description { get; set; }
        public int GroupType { get; set; }
    }
}
