using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BeFriendr.Network.UserProfiles.DTOs;
using BeFriendr.Network.UserProfiles.Entities;
using BeFriendr.Network.UserProfiles.Interfaces;
using BeFriendr.Network.UserProfiles.Requests;
using BeFriendr.Network.UserProfiles.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BeFriendr.Network.UserProfiles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]    
    public class UserProfileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserProfilesService _userProfileService;
        private readonly IPhotoService _photoService;
        public UserProfileController(IMapper mapper, IUserProfilesService userProfileService, IPhotoService photoService)
        {
            _photoService = photoService;
            _userProfileService = userProfileService;
            _mapper = mapper;
        }
        [HttpGet("{userName}", Name = nameof(GetByUserName))]
        public async Task<ActionResult<GetProfileResponse>> GetByUserName([FromRoute] string userName)
        {
            var profile = await _userProfileService.GetAsync(new GetProfileRequest { UserName = userName });
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
        [HttpPost]
        public async Task<ActionResult<CreateProfileResponse>> CreateAsync([FromBody] CreateProfileRequest request)
        {
            var profile = await _userProfileService.CreateAsync(request);
            var profileDto = _mapper.Map<UserProfileDto>(profile);
            return CreatedAtRoute(nameof(GetByUserName), new { userName = profile.UserName }, new CreateProfileResponse { UserProfileDto = profileDto });
        }
        [HttpPut]
        public async Task<ActionResult<UpdateProfileResponse>> UpdateAsync([FromBody] UpdateProfileRequest request)
        {
            var profile = await _userProfileService.UpdateAsync(request);
            var profileDto = _mapper.Map<UserProfileDto>(profile);
            return Ok(new UpdateProfileResponse { UserProfileDto = profileDto });
        }

        [HttpDelete("{userName}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] string userName)
        {
            await _userProfileService.DeleteAsync(new DeleteProfileRequest { UserName = userName });
            return Ok();
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<UploadPhotoResponse>> UploadPhotoAsync([FromForm] UploadPhotoRequest request)
        {
            var profile = await _userProfileService.GetAsync(new GetProfileRequest { UserName = request.UserName });
            var result = await _photoService.UploadPhotoAsync(request.File);

            Photo photo = new Photo
            {
                PublicID = result.PublicId,
                Url = result.SecureUrl.AbsoluteUri,
                IsMain = profile.Photos.Count == 0
            };

            await _userProfileService.AddPhotoAsync(profile, photo);
            var photoDto = _mapper.Map<PhotoDto>(photo);
            return Ok(new UploadPhotoResponse { PhotoDto = photoDto });
        }

    }
}