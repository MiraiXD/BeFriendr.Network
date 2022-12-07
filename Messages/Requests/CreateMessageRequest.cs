using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Network.Messages.Requests
{
    public class CreateMessageRequest
    {
        [Required]
        public string RecipientUserName { get; set; }
        [StringLength(1000, MinimumLength =1)]
        public string Content { get; set; }
        [Required]
        public DateTime DateSent { get; set; }
    }
}