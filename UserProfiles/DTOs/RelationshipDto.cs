using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.UserProfiles.Entities;

namespace BeFriendr.Network.UserProfiles.DTOs
{
    public class RelationshipDto
    {
        public string SenderUserName { get; set; }
        public string ReceiverUserName { get; set; }
        public RelationshipStatus Status { get; set; }
    }
}