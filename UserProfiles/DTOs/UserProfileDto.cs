using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.UserProfiles.Entities;

namespace BeFriendr.Network.UserProfiles.DTOs
{
    public class UserProfileDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MainPhotoUrl { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public int FriendsCount { get; set; }
        public ProfileThumbnailDto[] SelectedFriends { get; set; }
        public string RelationshipWithCurrentUser { get; set; }
    }
}