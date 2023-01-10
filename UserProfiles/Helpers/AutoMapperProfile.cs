using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BeFriendr.Common.Messages;
using BeFriendr.Network.UserProfiles.DTOs;
using BeFriendr.Network.UserProfiles.Entities;
using BeFriendr.Network.UserProfiles.Requests;

namespace BeFriendr.Network.UserProfiles.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserProfile, ProfileThumbnailDto>()
            .ForMember(dto=>dto.MainPhotoUrl, options=> options.MapFrom(src=> src.GetMainPhotoUrl()));
            CreateMap<UserProfile, UserProfileDto>()
             .ForMember(dto => dto.MainPhotoUrl, options => options.MapFrom(src => src.GetMainPhotoUrl()))
            .ForMember(dto => dto.FriendsCount, options => options.MapFrom(src=>src.GetFriendsCount()))
            .ForMember(dto=>dto.SelectedFriends, options=>options.MapFrom(src=>  src.GetSelectedFriends(9)
            .Select(profile=> new ProfileThumbnailDto{
                UserName = profile.UserName,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                MainPhotoUrl = profile.GetMainPhotoUrl()
            })
            .ToArray()))
            .AfterMap<SetRelationshipWithCurrentUserMappingAction>();            
            
            CreateMap<CreateProfileRequest, UserProfile>();
            CreateMap<UpdateProfileRequest, UserProfile>();
            CreateMap<DeleteProfileRequest, UserProfile>();
            CreateMap<Photo, PhotoDto>();
            CreateMap<AccountCreatedMessage, CreateProfileRequest>();
            CreateMap<Relationship, RelationshipDto>()
            .ForMember(dto => dto.SenderUserName, options => options.MapFrom(src => src.SendingProfile.UserName))
            .ForMember(dto => dto.ReceiverUserName, options => options.MapFrom(src => src.ReceivingProfile.UserName));
        }
    }
}