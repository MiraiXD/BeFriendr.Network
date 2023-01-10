using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BeFriendr.Common;
using BeFriendr.Network.Common;
using BeFriendr.Network.UserProfiles.Entities;
using BeFriendr.Network.UserProfiles.Exceptions;
using BeFriendr.Network.UserProfiles.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeFriendr.Network.UserProfiles.Services
{
    public class RelationshipService : IRelationshipService
    {
        private readonly IRelationshipRepository _relationshipRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserProfilesService _userProfilesService;
        public RelationshipService(IRelationshipRepository relationshipRepository, IUserProfilesService userProfilesService, IHttpContextAccessor httpContextAccessor)
        {
            _userProfilesService = userProfilesService;
            _httpContextAccessor = httpContextAccessor;
            _relationshipRepository = relationshipRepository;

        }
        public Relationship CreateRelationship(UserProfile sendingProfile, UserProfile receivingProfile)
        {
            var relationship = new Relationship
            {
                SendingProfile = sendingProfile,
                SendingProfileID = sendingProfile.ID,
                ReceivingProfile = receivingProfile,
                ReceivingProfileID = receivingProfile.ID,
            };
            _relationshipRepository.Insert(relationship);
            return relationship;
        }
        private string GetFriendName(Relationship relationship, string userName)
        {
            if (relationship.SendingProfile.UserName == userName) return relationship.ReceivingProfile.UserName;
            else return relationship.SendingProfile.UserName;
        }
        public async Task<IEnumerable<UserProfile>> GetFriendsOfUserAsync(string userName, int pageSize, int pageNumber)
        {
            var queryable = _relationshipRepository.AsQueryable()
            .Where(r => (r.ReceivingProfile.UserName == userName || r.SendingProfile.UserName == userName) && r.Status == RelationshipStatus.Accepted)
            .Include(r => r.SendingProfile)
            .Include(r => r.ReceivingProfile)
            .Select(r=> r.SendingProfile.UserName == userName ? r.ReceivingProfile.UserName : r.SendingProfile.UserName)
            .OrderBy(userName => userName);

            var names = await PagedList<string>.CreateAsync(queryable, pageNumber, pageSize);
            var friends = await _userProfilesService.GetManyByUserNamesAsync(names);

            return friends;
            // var queryable = _relationshipRepository.AsQueryable()
            // .Where(r => (r.ReceivingProfile.UserName == userName || r.SendingProfile.UserName == userName) && r.Status == RelationshipStatus.Accepted)
            // .OrderBy(r=>r, new FriendsAlphabeticComparer(userName))
            // .Include(r => r.SendingProfile)
            // .Include(r => r.ReceivingProfile);

            // var relationships = await PagedList<Relationship>.CreateAsync(queryable, pageNumber, pageSize);            
            // var names = relationships.Select(r =>
            // {
            //     if (r.SendingProfile.UserName == userName) return r.ReceivingProfile.UserName;
            //     else return r.SendingProfile.UserName;
            // });
            // var friends = await _userProfilesService.GetManyByUserNamesAsync(names);
            // friends = friends.OrderBy(profile => profile.UserName);

            // return friends;
        }

        public async Task<IEnumerable<Relationship>> GetReceivedFriendRequestsForUserAsync(string userName, int pageSize, int pageNumber)
        {
            var queryable = _relationshipRepository.AsQueryable()
            // only show those not yet decided - hide Accepted, Rejected, Dismissed, Blocked
            .Where(r => r.ReceivingProfile.UserName == userName && (r.Status == RelationshipStatus.None || r.Status == RelationshipStatus.Read))
            .OrderByDescending(r => r.DateSent)
            .Include(r => r.SendingProfile)
            .Include(r => r.ReceivingProfile);

            return await PagedList<Relationship>.CreateAsync(queryable, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Relationship>> GetSentFriendRequestsForUserAsync(string userName, int pageSize, int pageNumber)
        {
            var queryable = _relationshipRepository.AsQueryable()
            // show Dismissed, Rejected and Blocked - you shouldn't know you've be Dismissed, Rejected or Blocked
            .Where(r => r.SendingProfile.UserName == userName && (r.Status != RelationshipStatus.Accepted))
            .OrderByDescending(r => r.DateSent)
            .Include(r => r.SendingProfile)
            .Include(r => r.ReceivingProfile);

            return await PagedList<Relationship>.CreateAsync(queryable, pageNumber, pageSize);
        }

        public async Task<Relationship> SetRelationshipStatusAsync(string senderUserName, string receiverUserName, string status)
        {
            if (!Enum.TryParse<RelationshipStatus>(status, out RelationshipStatus parsedStatus))
                throw new RelationshipExceptions.SetStatus.IncorrectStatusValueException(status);

            var relationship = await _relationshipRepository.AsQueryable()
            .Include(r => r.SendingProfile)
            .Include(r => r.ReceivingProfile)
            .FirstOrDefaultAsync(r => r.SendingProfile.UserName == senderUserName && r.ReceivingProfile.UserName == receiverUserName);

            if (relationship == null)
                throw new RelationshipExceptions.SetStatus.NotFoundException(senderUserName, receiverUserName);

            var loggedInUserName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (parsedStatus == RelationshipStatus.Canceled)
            {
                if (senderUserName != loggedInUserName) throw new RelationshipExceptions.SetStatus.UnauthorizedSenderException(loggedInUserName, relationship, parsedStatus);
                else if (relationship.Status == RelationshipStatus.Accepted || relationship.Status == RelationshipStatus.Rejected)
                    throw new RelationshipExceptions.SetStatus.ActionNotAllowed(relationship, parsedStatus);

            }
            else if (parsedStatus == RelationshipStatus.Read ||
            parsedStatus == RelationshipStatus.Accepted ||
            parsedStatus == RelationshipStatus.Rejected ||
            parsedStatus == RelationshipStatus.Blocked ||
            parsedStatus == RelationshipStatus.Dismissed)
            {
                if (receiverUserName != loggedInUserName)
                    throw new RelationshipExceptions.SetStatus.UnauthorizedReceiverException(loggedInUserName, relationship, parsedStatus);
            }


            relationship.Status = parsedStatus;
            return relationship;
        }
        public class FriendsAlphabeticComparer : IComparer<Relationship>
        {
            private readonly string _userName;
            public FriendsAlphabeticComparer(string userName)
            {
                _userName = userName;
            }
            public int Compare(Relationship x, Relationship y)
            {
                string xFriendUserName = x.SendingProfile.UserName != _userName ? x.SendingProfile.UserName : x.ReceivingProfile.UserName;
                string yFriendUserName = y.SendingProfile.UserName != _userName ? y.SendingProfile.UserName : y.ReceivingProfile.UserName;
                return xFriendUserName.CompareTo(yFriendUserName);
            }
        }
    }
}