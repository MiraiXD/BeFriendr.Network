using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Network.UserProfiles.Entities
{
    public class Relationship
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int SendingProfileID { get; set; }
        public UserProfile SendingProfile { get; set; }
        [Required]
        public int ReceivingProfileID { get; set; }
        public  UserProfile ReceivingProfile { get; set; }
        public int? BlockedByProfileID { get; set; }
        public RelationshipStatus Status { get; set; } = RelationshipStatus.None;
        public DateTime DateSent { get; set; } = DateTime.UtcNow;
    }
    public enum RelationshipStatus
    {
        None,
        Read,
        Accepted,
        Rejected,
        Dismissed,
        Blocked
    }
}