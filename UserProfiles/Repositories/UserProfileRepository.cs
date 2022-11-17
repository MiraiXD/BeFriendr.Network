using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Common;
using BeFriendr.Network.UserProfiles.Entities;
using BeFriendr.Network.UserProfiles.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeFriendr.Network.UserProfiles.Repositories
{
    public sealed class UserProfilesRepository : CrudRepository<UserProfile>, IUserProfilesRepository
    {
        public UserProfilesRepository(DbContext context) : base(context)
        {
        }
        public async Task<UserProfile> GetByUserNameAsync(string userName)
        {
            return await Entities
            .Where(x => x.UserName == userName)
            .FirstOrDefaultAsync();
        }
    }
}