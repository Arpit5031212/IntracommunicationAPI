using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace IntraCommunicationWebApi.Model
{
    [Index(nameof(UserId), nameof(PostId), Name = "UNIQUE_LIKE", IsUnique = true)]
    [Index(nameof(UserId), nameof(PostId), Name = "UX_likes_table_user_post", IsUnique = true)]
    public partial class Like
    {
        [Column("PostID")]
        public int PostId { get; set; }
        public int UserId { get; set; }
        [Key]
        public int LikeId { get; set; }

        [ForeignKey(nameof(PostId))]
        [InverseProperty("Likes")]
        public virtual Post Post { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserProfile.Likes))]
        public virtual UserProfile User { get; set; }
    }
}
