using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.Messages.DTOs;

namespace BeFriendr.Network.Messages.Responses
{
    public class CreateMessageResponse
    {
        public MessageDto MessageDto { get; set; }
    }
}