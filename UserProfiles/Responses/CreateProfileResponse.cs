using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.UserProfiles.DTOs;

namespace BeFriendr.Network.UserProfiles.Responses
{
    public class CreateProfileResponse
    {
        public UserProfileDto UserProfileDto { get; set; }
    }
}