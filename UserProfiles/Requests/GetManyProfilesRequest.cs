using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Network.UserProfiles.Requests
{
    public class GetManyProfilesRequest
    {
        private const int MAX_PAGE_SIZE = 50;
        private const int MIN_PAGE_SIZE = 5;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Clamp(value, MIN_PAGE_SIZE, MAX_PAGE_SIZE);
        }
        public int PageNumber { get; set; } = 1;
        public string UserName { get; set; } = "";
    }
}