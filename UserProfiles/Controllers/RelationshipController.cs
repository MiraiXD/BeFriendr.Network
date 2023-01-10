using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BeFriendr.Network.Common;
using BeFriendr.Network.UserProfiles.DTOs;
using BeFriendr.Network.UserProfiles.Entities;
using BeFriendr.Network.UserProfiles.Exceptions;
using BeFriendr.Network.UserProfiles.Requests;
using BeFriendr.Network.UserProfiles.Responses;
using BeFriendr.Network.UserProfiles.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BeFriendr.Network.UserProfiles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RelationshipController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserProfilesService _userProfileService;
        private readonly IRelationshipService _relationshipService;
        private readonly UnitOfWork _unitOfWork;
        public RelationshipController(IUserProfilesService userProfilesService, IRelationshipService relationshipService, UnitOfWork unitOfWork, IMapper mapper)
        {
            _userProfileService = userProfilesService;
            _relationshipService = relationshipService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("add-friend")]
        public async Task<ActionResult> AddFriendAsync([FromQuery] AddFriendRequest request)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sendingProfile = await _userProfileService.GetByUserNameAsync(userName);
            var receivingProfile = await _userProfileService.GetByUserNameAsync(request.UserName);
            if (receivingProfile == null) throw new RelationshipExceptions.Create.UserDoesNotExistException(request.UserName);

            var relationship = _relationshipService.CreateRelationship(sendingProfile, receivingProfile);
            await _unitOfWork.SaveAllAsync();
            return Ok();
        }
        [HttpGet("friend-requests-inbox")]
        public async Task<ActionResult<RelationshipDto[]>> GetReceivedFriendRequests([FromQuery] PageableRequest request)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requests = await _relationshipService.GetReceivedFriendRequestsForUserAsync(userName, request.PageSize, request.PageNumber);
            var relationshipDtos = _mapper.Map<RelationshipDto[]>(requests);
            return Ok(relationshipDtos);
        }
        [HttpGet("friend-requests-outbox")]
        public async Task<ActionResult<RelationshipDto[]>> GetSentFriendRequests([FromQuery] PageableRequest request)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requests = await _relationshipService.GetSentFriendRequestsForUserAsync(userName, request.PageSize, request.PageNumber);
            var relationshipDtos = _mapper.Map<RelationshipDto[]>(requests);
            return Ok(relationshipDtos);
        }
        [HttpGet("friends")]
        public async Task<ActionResult<UserProfileDto[]>> GetFriends([FromQuery] PageableRequest request)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profiles = await _relationshipService.GetFriendsOfUserAsync(userName, request.PageSize, request.PageNumber);
            var profileDtos = _mapper.Map<ProfileThumbnailDto[]>(profiles);
            return Ok(profileDtos);
        }
        [HttpPut("set-status")]
        public async Task<ActionResult<RelationshipDto>> SetRelationshipStatus([FromBody] SetRelationshipStatusRequest request)
        {
            var relationship = await _relationshipService.SetRelationshipStatusAsync(request.SenderUserName, request.ReceiverUserName, request.Status);
            await _unitOfWork.SaveAllAsync();
            return Ok(_mapper.Map<RelationshipDto>(relationship));
        }
    }
}