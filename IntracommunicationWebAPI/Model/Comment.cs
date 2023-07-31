using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace IntraCommunicationWebApi.Model
{
    public partial class Comment
    {
        [Key]
        [Column("CommentID")]
        public int CommentId { get; set; }
        [Column("Commented_By")]
        public int CommentedBy { get; set; }
        [Column("PostID")]
        public int PostId { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string CommentDescription { get; set; }
        [Column("Commented_At", TypeName = "datetime")]
        public DateTime CommentedAt { get; set; }

        [ForeignKey(nameof(CommentedBy))]
        [InverseProperty(nameof(UserProfile.Comments))]
        public virtual UserProfile CommentedByNavigation { get; set; }
    }
}
