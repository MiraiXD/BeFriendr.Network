using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BeFriendr.Common;
using BeFriendr.Network.Messages.Entities;
using BeFriendr.Network.Messages.Repositories;
using BeFriendr.Network.Messages.Requests;
using BeFriendr.Network.UserProfiles.Services;
using Microsoft.EntityFrameworkCore;

namespace BeFriendr.Network.Messages.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IUserProfilesService _userProfilesService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MessageService(IMapper mapper, IMessageRepository messageRepository, IUserProfilesService userProfilesService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userProfilesService = userProfilesService;
            _mapper = mapper;
            _messageRepository = messageRepository;

        }
        public async Task<Message> CreateAsync(CreateMessageRequest request)
        {
            var senderUserName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sender = await _userProfilesService.GetByUserNameAsync(senderUserName);

            var recipientUserName = request.RecipientUserName;
            var recipient = await _userProfilesService.GetByUserNameAsync(recipientUserName);

            var message = new Message
            {
                Content = request.Content,
                DateRead = null,
                DateSent = request.DateSent,
                RecipientProfile = recipient,
                RecipientUserName = recipientUserName,
                RecipientID = recipient.ID,
                SenderProfile = sender,
                SenderUserName = senderUserName,
                SenderID = sender.ID,
            };
            _messageRepository.Insert(message);

            return message;
        }

        public async Task<IEnumerable<Message>> GetThreadAsync(GetThreadRequest request)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var queryable = _messageRepository.AsQueryable()
            .Where(message => (message.SenderUserName == userName && message.RecipientUserName == request.UserName) ||
            (message.SenderUserName == request.UserName && message.RecipientUserName == userName))
            .OrderByDescending(message => message.DateSent);

            return await PagedList<Message>.CreateAsync(queryable, request.PageNumber, request.PageSize);
        }

        public async Task<IEnumerable<Message>> GetUnreadAsync()
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var queryable = _messageRepository.AsQueryable()
            .Where(message => message.DateRead == null && message.RecipientUserName == userName)
            .OrderByDescending(message => message.DateSent);

            return await queryable.ToListAsync();
        }

        // public async Task<IEnumerable<Message>> GetManyAsync(GetManyMessagesRequest request)
        // {
        //     var queryable = _messageRepository.AsQueryable();
        //     if (!string.IsNullOrEmpty(request.SenderUserName))
        //         queryable = queryable.Where(message => message.SenderUserName == request.SenderUserName);

        //     if (!string.IsNullOrEmpty(request.RecipientUserName))
        //         queryable = queryable.Where(message => message.RecipientUserName == request.RecipientUserName);

        //     return await PagedList<Message>.CreateAsync(queryable, request.PageNumber, request.PageSize);
        // }
    }
}