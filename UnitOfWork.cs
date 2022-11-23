using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Common;
using BeFriendr.Network.UserProfiles.Interfaces;

namespace BeFriendr.Network
{
    public class UnitOfWork : BaseUnitOfWork<NetworkDbContext>
    {
        public IUserProfilesRepository UserProfilesRepository {get;}
        public UnitOfWork(NetworkDbContext dbContext, IUserProfilesRepository userProfilesRepository) : base(dbContext)
        {
            UserProfilesRepository=userProfilesRepository;
        }
    }
}