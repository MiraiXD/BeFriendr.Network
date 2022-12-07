using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Network.Messages.DTOs
{
    public class MessageDto
    {
        public string SenderUserName { get; set; }   
        public string SenderPhotoUrl { get; set; }        
        public string RecipientUserName { get; set; }
        public string RecipientPhotoUrl { get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime DateSent { get; set; } 
    }
}