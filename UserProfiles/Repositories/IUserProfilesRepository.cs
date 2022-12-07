using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Common;
using BeFriendr.Network.UserProfiles.Entities;

namespace BeFriendr.Network.UserProfiles.Repositories
{
    public interface IUserProfilesRepository : ICrudRepository<UserProfile>
    {
        Task<UserProfile> GetByUserNameAsync(string userName);
    }
}