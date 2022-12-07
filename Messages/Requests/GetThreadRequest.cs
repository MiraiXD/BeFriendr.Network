using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.Common;

namespace BeFriendr.Network.Messages.Requests
{
    public class GetThreadRequest : PageableRequest
    {
        public string UserName { get; set; }
    }
}