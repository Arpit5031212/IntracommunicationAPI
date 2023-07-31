using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace IntraCommunicationWebApi.Model
{
    [Table("Group_Members")]
    public partial class GroupMember
    {
        [Column("MemberID")]
        public int MemberId { get; set; }
        [Column("GroupID")]
        public int GroupId { get; set; }
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty("GroupMembers")]
        public virtual Group Group { get; set; }
        [ForeignKey(nameof(MemberId))]
        [InverseProperty(nameof(UserProfile.GroupMembers))]
        public virtual UserProfile Member { get; set; }
    }
}
