using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.Messages.Entities;
using BeFriendr.Network.UserProfiles.DTOs;

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

        public ICollection<Photo> Photos { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<Relationship> RelationshipsSent { get; set; }
        public ICollection<Relationship> RelationshipsReceived { get; set; }
        public string GetMainPhotoUrl()
        {
            var mainPhoto = Photos.FirstOrDefault(photo => photo.IsMain);
            return mainPhoto != null ? mainPhoto.Url : "";
        }
        public int GetFriendsCount()
        {
            int friendsCount = 0;
            friendsCount += RelationshipsSent.Where(r => r.Status == RelationshipStatus.Accepted).Count();
            friendsCount += RelationshipsReceived.Where(r => r.Status == RelationshipStatus.Accepted).Count();
            return friendsCount;
        }
        public IEnumerable<UserProfile> GetSelectedFriends(int count)
        {
            var friends = RelationshipsSent
            .Where(r => r.Status == RelationshipStatus.Accepted)

            .Take(count)
            .Select(r => r.ReceivingProfile);

            int friendsCount = friends.Count();
            if (friendsCount < count)
            {
                friends = friends.Concat(RelationshipsReceived
                .Where(r => r.Status == RelationshipStatus.Accepted)
                .Take(count - friendsCount)
                .Select(r => r.SendingProfile));
            }
            return friends;
        }
        public RelationshipStatus? GetRelationshipStatusWithUser(string userName)
        {
            var relationship = RelationshipsReceived.FirstOrDefault(r => r.SendingProfile.UserName == userName);
            if (relationship != null) return relationship.Status;
            relationship = RelationshipsSent.FirstOrDefault(r => r.ReceivingProfile.UserName == userName);
            if (relationship != null) return relationship.Status;

            return null;
        }
    }
}