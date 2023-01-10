using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BeFriendr.Network.UserProfiles.DTOs;
using BeFriendr.Network.UserProfiles.Entities;

namespace BeFriendr.Network.UserProfiles.Helpers
{
    public class SetRelationshipWithCurrentUserMappingAction : IMappingAction<UserProfile, UserProfileDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SetRelationshipWithCurrentUserMappingAction(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Process(UserProfile source, UserProfileDto destination, ResolutionContext context)
        {
            var currentUserName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var status = source.GetRelationshipStatusWithUser(currentUserName);
            destination.RelationshipWithCurrentUser = status != null ? status.ToString() : null;
        }
    }
}