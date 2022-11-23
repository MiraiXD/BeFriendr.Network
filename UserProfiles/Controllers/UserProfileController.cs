using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BeFriendr.Network.UserProfiles.DTOs;
using BeFriendr.Network.UserProfiles.Entities;
using BeFriendr.Network.UserProfiles.Interfaces;
using BeFriendr.Network.UserProfiles.Requests;
using BeFriendr.Network.UserProfiles.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFriendr.Network.UserProfiles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]    
    public class UserProfileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserProfilesService _userProfileService;
        private readonly IPhotoService _photoService;
        private readonly UnitOfWork _unitOfWork;
        public UserProfileController(IMapper mapper, IUserProfilesService userProfileService, IPhotoService photoService, UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _photoService = photoService;
            _userProfileService = userProfileService;
            _mapper = mapper;
        }
        [HttpGet("{userName}", Name = nameof(GetByUserName))]
        public async Task<ActionResult<GetProfileResponse>> GetByUserName([FromRoute] string userName)
        {
            var profile = await _userProfileService.GetAsync(new GetProfileRequest { UserName = userName });
            if(profile == null) return NotFound();
            var profileDto = _mapper.Map<UserProfileDto>(profile);
            return Ok(new GetProfileResponse { UserProfileDto = profileDto });
        }
        [HttpGet]
        public async Task<ActionResult<GetManyProfilesResponse>> GetManyAsync([FromQuery] GetManyProfilesRequest request)
        {
            var profiles = await _userProfileService.GetManyAsync(request);
            IEnumerable<UserProfileDto> profileDtos = _mapper.Map<IEnumerable<UserProfileDto>>(profiles);
            return Ok(new GetManyProfilesResponse { ProfileDtos = profileDtos.ToArray() });
        }
        // [HttpPost]
        // public async Task<ActionResult<CreateProfileResponse>> CreateAsync([FromBody] CreateProfileRequest request)
        // {
        //     var profile = await _userProfileService.CreateAsync(request);
        //     var profileDto = _mapper.Map<UserProfileDto>(profile);
        //     return CreatedAtRoute(nameof(GetByUserName), new { userName = profile.UserName }, new CreateProfileResponse { UserProfileDto = profileDto });
        // }
        [HttpPut]
        public async Task<ActionResult<UpdateProfileResponse>> UpdateAsync([FromBody] UpdateProfileRequest request)
        {
            string userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await _userProfileService.UpdateAsync(userName, request);
            await _unitOfWork.SaveAllAsync();
            var profileDto = _mapper.Map<UserProfileDto>(profile);
            return Ok(new UpdateProfileResponse { UserProfileDto = profileDto });
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync()
        {
            string userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userProfileService.DeleteAsync(userName, new DeleteProfileRequest());
            await _unitOfWork.SaveAllAsync();
            return Ok();
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<UploadPhotoResponse>> UploadPhotoAsync([FromForm] UploadPhotoRequest request)
        {
            string userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await _userProfileService.GetAsync(new GetProfileRequest { UserName = userName });
            var result = await _photoService.UploadPhotoAsync(request.File);

            Photo photo = new Photo
            {
                PublicID = result.PublicId,
                Url = result.SecureUrl.AbsoluteUri,
                IsMain = profile.Photos.Count == 0
            };

            await _userProfileService.AddPhotoAsync(profile, photo);
            await _unitOfWork.SaveAllAsync();
            var photoDto = _mapper.Map<PhotoDto>(photo);
            return Ok(new UploadPhotoResponse { PhotoDto = photoDto });
        }

    }
}