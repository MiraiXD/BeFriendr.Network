using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.Common;

namespace BeFriendr.Network.UserProfiles.Requests
{
    public class GetManyProfilesRequest : PageableRequest
    {        
        public string UserName { get; set; } = "";
    }
}