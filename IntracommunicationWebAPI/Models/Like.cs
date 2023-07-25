using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace IntraCommunicationWebApi.Models
{
    [Index(nameof(PostId), Name = "IX_Likes_PostID")]
    [Index(nameof(UserId), Name = "IX_Likes_UserId")]
    public partial class Like
    {
        [Column("PostID")]
        public int PostId { get; set; }
        public int UserId { get; set; }
        [Key]
        [Column("LikeID")]
        public int LikeId { get; set; }

        [ForeignKey(nameof(PostId))]
        [InverseProperty("Likes")]
        public virtual Post Post { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserProfile.Likes))]
        public virtual UserProfile User { get; set; }
    }
}
