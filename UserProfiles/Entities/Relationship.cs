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
        None, // unread
        Read, // Receiver has seen the request, didnt do anything yet
        Accepted, // Receiver accepted request and is now friends with the Sender
        Rejected, // Receiver rejected and does not want to be friends with the Sender
        Dismissed, // Receiver wants to ignore the request
        Blocked, // Receiver wants to block the Sender
        Canceled // Sender canceled the request after sending - can only cancel if not Accepted or Rejected
    }
}