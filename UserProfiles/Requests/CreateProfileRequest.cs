using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Network.UserProfiles.Requests
{
    public class CreateProfileRequest
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [RegularExpression("male|female")]
        public string Gender { get; set; }
    }
}