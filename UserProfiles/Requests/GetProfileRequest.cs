using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Network.UserProfiles.Requests
{
    public class GetProfileRequest
    {
        public string UserName { get; set; }
        public int? MyProperty { get; set; }
    }
}