using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Network.UserProfiles.Requests
{
    public class UploadPhotoRequest
    {
        // [Required]
        // public string UserName { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}