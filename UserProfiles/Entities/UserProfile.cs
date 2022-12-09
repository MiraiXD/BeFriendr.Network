using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.Messages.Entities;

namespace BeFriendr.Network.UserProfiles.Entities
{
    public class UserProfile
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(15)]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Gender { get; set; }

        public  ICollection<Photo> Photos { get; set; }
        public  ICollection<Message> MessagesSent { get; set; }
        public  ICollection<Message> MessagesReceived { get; set; }
        public  ICollection<Relationship> RelationshipsSent { get; set; }
        public  ICollection<Relationship> RelationshipsReceived { get; set; }
    }
}