using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace IntraCommunicationWebApi.Models
{
    [Index(nameof(CreatedBy), Name = "IX_Groups_Created_By")]
    [Index(nameof(GroupName), Name = "UK_Groups_Group_Name", IsUnique = true)]
    public partial class Group
    {
        public Group()
        {
            GroupInvitesRequests = new HashSet<GroupInvitesRequest>();
            GroupMembers = new HashSet<GroupMember>();
            Posts = new HashSet<Post>();
        }

        [Key]
        [Column("GroupID")]
        public int GroupId { get; set; }
        [Required]
        [Column("Group_Name")]
        [StringLength(50)]
        public string GroupName { get; set; }
        [Required]
        [Column("Group_Description", TypeName = "text")]
        public string GroupDescription { get; set; }
        [Column("Group_Type")]
        public int GroupType { get; set; }
        [Column("Created_At", TypeName = "date")]
        public DateTime CreatedAt { get; set; }
        [Column("Created_By")]
        public int CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(UserProfile.Groups))]
        public virtual UserProfile CreatedByNavigation { get; set; }
        [InverseProperty(nameof(GroupInvitesRequest.Group))]
        public virtual ICollection<GroupInvitesRequest> GroupInvitesRequests { get; set; }
        [InverseProperty(nameof(GroupMember.Group))]
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        [InverseProperty(nameof(Post.PostedOnGroupNavigation))]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
