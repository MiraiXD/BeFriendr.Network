using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.UserProfiles.Entities;

namespace BeFriendr.Network.Messages.Entities
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int SenderID { get; set; }
        [Required]
        public string SenderUserName { get; set; }         
        public UserProfile SenderProfile { get; set; }
        [Required]
        public int RecipientID { get; set; }
        [Required]
        public string RecipientUserName { get; set; }
        public UserProfile RecipientProfile { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime DateSent { get; set; }
    }
}