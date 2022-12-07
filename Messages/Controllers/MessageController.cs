using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BeFriendr.Network.Messages.DTOs;
using BeFriendr.Network.Messages.Requests;
using BeFriendr.Network.Messages.Responses;
using BeFriendr.Network.Messages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFriendr.Network.Messages.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MessageController(IMessageService messageService, IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _messageService = messageService;

        }
        [HttpPost]
        public async Task<ActionResult> CreateMessage([FromBody] CreateMessageRequest request)
        {
            var message = await _messageService.CreateAsync(request);
            await _unitOfWork.SaveAllAsync();
            return Ok(new CreateMessageResponse
            {
                MessageDto = _mapper.Map<MessageDto>(message)
            });
        }
        [HttpGet("thread")]
        public async Task<ActionResult<GetThreadResponse>> GetThread([FromQuery] GetThreadRequest request)
        {
            var messages = await _messageService.GetThreadAsync(request);

            foreach(var message in messages)
            {
                if(message.DateRead == null) message.DateRead=DateTime.UtcNow;
            }
            await _unitOfWork.SaveAllAsync();

            var response = new GetThreadResponse{
                Messages = _mapper.Map<MessageDto[]>(messages)
            };
            return Ok(response);
        }
        [HttpGet("unread")]
        public async Task<ActionResult<GetUnreadResponse>> GetUnread()
        {
            var messages = await _messageService.GetUnreadAsync();
            var response = new GetUnreadResponse{
                UnreadMessages = _mapper.Map<MessageDto[]>(messages)
            };
            return Ok(response);
        }
        // [HttpGet]
        // public async Task<ActionResult<GetMessagesWithProfileResponse>> GetMessagesBetweenProfiles([FromQuery] GetMessagesWithUserRequest request)
        // {
        //     var currentUserName = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //     var messageRequest = new GetManyMessagesRequest
        //     {
        //         PageNumber = request.PageNumber,
        //         PageSize = request.PageSize,
        //         RecipientUserName = currentUserName,
        //         SenderUserName = request.UserName
        //     };
        //     var messages = await _messageService.GetManyAsync(messageRequest);

        //     messageRequest.RecipientUserName = request.UserName;
        //     messageRequest.SenderUserName = currentUserName;
        //     var messages2 = await _messageService.GetManyAsync(messageRequest);
        //     messages = messages.Concat(messages2);
        // }
    }
}