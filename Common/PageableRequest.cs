using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Network.Common
{
    public class PageableRequest
    {
        public const int MAX_PAGE_SIZE = 50;
        public const int MIN_PAGE_SIZE = 5;
        //public const PageableRequest Default = new Page
        protected int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Clamp(value, MIN_PAGE_SIZE, MAX_PAGE_SIZE);
        }
        public int PageNumber { get; set; } = 1;
    }
}