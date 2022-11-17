using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Network.Authentication
{
    public class AuthenticationSettings
    {
        public string SecretKey { get; set; }
        public int ExpiresInMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}