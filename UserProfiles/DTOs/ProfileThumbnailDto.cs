using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Network.UserProfiles.DTOs
{
    public class ProfileThumbnailDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MainPhotoUrl { get; set; }
    }
}