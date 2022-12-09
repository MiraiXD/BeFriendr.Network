using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.Common;

namespace BeFriendr.Network.UserProfiles.Requests
{
    public class GetFriendRequestsRequest
    {
        public string SenderUserName { get; set; }
        public string ReceiverUserName { get; set; }
        public bool UnreadOnly { get; set; }
    }
}