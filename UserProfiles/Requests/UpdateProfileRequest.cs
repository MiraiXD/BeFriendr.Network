using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Network.UserProfiles.Requests
{
    public class UpdateProfileRequest
    {
        [Required]
        public string FindByUserName { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}