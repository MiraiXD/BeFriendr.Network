using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Network.UserProfiles.Entities
{
    [Table("Photos")]
    public class Photo
    {
        [Key]
        public int ID { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicID { get; set; }
        public UserProfile UserProfile { get; set; }
        public int ProfileID { get; set; }
    }
}