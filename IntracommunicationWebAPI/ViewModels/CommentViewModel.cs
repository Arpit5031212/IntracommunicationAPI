using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace IntraCommunicationWebApi.ViewModels
{
    public class CommentViewModel
    {
        [Required]
        public int CommentedBy { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public string CommentDescription { get; set; }
        [Required]
        public DateTime CommentedAt { get; set; }
    }
}
