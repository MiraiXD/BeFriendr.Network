using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BeFriendr.Common.Messages;
using BeFriendr.Network.UserProfiles.Interfaces;
using BeFriendr.Network.UserProfiles.Requests;
using MassTransit;

namespace BeFriendr.Network.Consumers
{
    public class AccountCreatedConsumer : IConsumer<AccountCreatedMessage>
    {
        private readonly IUserProfilesService _userProfilesService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        public AccountCreatedConsumer(IUserProfilesService userProfilesService, IMapper mapper, UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userProfilesService = userProfilesService;
        }
        public async Task Consume(ConsumeContext<AccountCreatedMessage> context)
        {
            var request = _mapper.Map<CreateProfileRequest>(context.Message);
            var userProfile = await _userProfilesService.CreateAsync(request);
            await _unitOfWork.SaveAllAsync();
        }
    }
    public class AccountCreatedConsumerDefinition : ConsumerDefinition<AccountCreatedConsumer>
    {
        public AccountCreatedConsumerDefinition()
        {
            EndpointName = "";
        }
    }
}