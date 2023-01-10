using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.Common;
using BeFriendr.Network.UserProfiles.Entities;

namespace BeFriendr.Network.UserProfiles.Services
{
    public interface IRelationshipService
    {
        Relationship CreateRelationship(UserProfile sendingProfile, UserProfile receivingProfile);
        //Task<IEnumerable<Relationship>> GetRelationshipsAsync(string sender = null, string receiver = null, RelationshipStatus? status = null, PageableRequest? pageableRequest = null);        
        Task<IEnumerable<Relationship>> GetReceivedFriendRequestsForUserAsync(string userName, int pageSize, int pageNumber);
        Task<IEnumerable<Relationship>> GetSentFriendRequestsForUserAsync(string userName, int pageSize, int pageNumber);
        Task<IEnumerable<UserProfile>> GetFriendsOfUserAsync(string userName, int pageSize, int pageNumber);
        Task<Relationship> SetRelationshipStatusAsync(string senderUserName, string receiverUserName, string status);
    }
}