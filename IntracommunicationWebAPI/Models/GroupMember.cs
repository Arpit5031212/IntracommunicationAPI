using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace IntraCommunicationWebApi.Models
{
    [Table("Group_Members")]
    [Index(nameof(GroupId), Name = "IX_Group_Members_GroupID")]
    [Index(nameof(MemberId), Name = "IX_Group_Members_MemberID")]
    public partial class GroupMember
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("MemberID")]
        public int MemberId { get; set; }
        [Column("GroupID")]
        public int GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty("GroupMembers")]
        public virtual Group Group { get; set; }

        [ForeignKey(nameof(MemberId))]
        [InverseProperty(nameof(UserProfile.GroupMembers))]
        public virtual UserProfile Member { get; set; }
    }
}
