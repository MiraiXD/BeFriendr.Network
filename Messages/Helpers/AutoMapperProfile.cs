using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BeFriendr.Network.Messages.DTOs;
using BeFriendr.Network.Messages.Entities;
using BeFriendr.Network.Messages.Requests;

namespace BeFriendr.Network.Messages.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Message, MessageDto>()
            .ForMember(message => message.SenderPhotoUrl, options => options.MapFrom(message => message.SenderProfile.GetMainPhotoUrl()))
            .ForMember(message => message.RecipientPhotoUrl, options => options.MapFrom(message => message.RecipientProfile.GetMainPhotoUrl()));
            CreateMap<CreateMessageRequest, Message>();
        }
    }
}