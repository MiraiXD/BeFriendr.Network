using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.Messages.Entities;
using BeFriendr.Network.Messages.Requests;

namespace BeFriendr.Network.Messages.Services
{
    public interface IMessageService
    {
        //  Task<Message> GetAsync(GetProfileRequest request);
        Task<IEnumerable<Message>> GetUnreadAsync();
        Task<IEnumerable<Message>> GetThreadAsync(GetThreadRequest request);
        //Task<IEnumerable<Message>> GetManyAsync(GetManyMessagesRequest request);
        Task<Message> CreateAsync(CreateMessageRequest request);
        //Task DeleteAsync(string userName, DeleteProfileRequest request);
    }
}