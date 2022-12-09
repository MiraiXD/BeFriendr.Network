using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Common;
using BeFriendr.Network.UserProfiles.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeFriendr.Network.UserProfiles.Repositories
{
    public class RelationshipRepository : CrudRepository<Relationship>, IRelationshipRepository
    {
        public RelationshipRepository(DbContext context) : base(context)
        {
        }
        public async Task<Relationship> GetByUserName(string userName)
        {
            return await Entities
            .Where(r => r.SendingProfile.UserName == userName || r.ReceivingProfile.UserName == userName)
            .FirstOrDefaultAsync();
        }
    }
}