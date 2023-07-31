using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace IntraCommunicationWebApi.Model
{
    [Table("Group_Invites_Requests")]
    [Index(nameof(SentTo), nameof(GroupId), Name = "UX_Group_Invites_Request_table_sentTo_GroupID", IsUnique = true)]
    public partial class GroupInvitesRequest
    {
        [Key]
        [Column("InviteID")]
        public int InviteId { get; set; }
        [Column("Sent_to")]
        public int SentTo { get; set; }
        [Column("GroupID")]
        public int GroupId { get; set; }
        [Column("isAccepted")]
        public bool IsAccepted { get; set; }
        [Column("isApproved")]
        public bool IsApproved { get; set; }
        [Column("Created_by")]
        public int CreatedBy { get; set; }
        [Column("Created_at", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column("Updated_at", TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(UserProfile.GroupInvitesRequestCreatedByNavigations))]
        public virtual UserProfile CreatedByNavigation { get; set; }
        [ForeignKey(nameof(GroupId))]
        [InverseProperty("GroupInvitesRequests")]
        public virtual Group Group { get; set; }
        [ForeignKey(nameof(SentTo))]
        [InverseProperty(nameof(UserProfile.GroupInvitesRequestSentToNavigations))]
        public virtual UserProfile SentToNavigation { get; set; }
    }
}
