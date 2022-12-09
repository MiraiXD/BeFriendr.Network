using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Network.UserProfiles.Requests
{
    public class SetRelationshipStatusRequest
    {
        public string SenderUserName { get; set; }
        public string ReceiverUserName { get; set; }
        public string Status { get; set; }
    }
}