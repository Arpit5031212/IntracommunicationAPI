using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace IntraCommunicationWebApi.Model
{
    public partial class Post
    {
        public Post()
        {
            Likes = new HashSet<Like>();
        }

        [Key]
        [Column("PostID")]
        public int PostId { get; set; }
        [Required]
        [Column("Post_Type")]
        [StringLength(1)]
        public string PostType { get; set; }
        public int PostedOnGroup { get; set; }
        [Column(TypeName = "date")]
        public DateTime PostedAt { get; set; }
        public int PostedBy { get; set; }
        [Required]
        [Column("Post description", TypeName = "text")]
        public string PostDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndTime { get; set; }
        [Column("URL", TypeName = "text")]
        public string Url { get; set; }

        [ForeignKey(nameof(PostedOnGroup))]
        [InverseProperty(nameof(Group.Posts))]
        public virtual Group PostedOnGroupNavigation { get; set; }
        [InverseProperty(nameof(Like.Post))]
        public virtual ICollection<Like> Likes { get; set; }
    }
}
