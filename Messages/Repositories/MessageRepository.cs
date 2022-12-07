using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Common;
using BeFriendr.Network.Messages.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeFriendr.Network.Messages.Repositories
{
    public class MessageRepository : CrudRepository<Message>, IMessageRepository
    {
        public MessageRepository(DbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Message>> GetMessagesBetweenProfiles(int currentProfileID, int otherProfileID)
        {
            throw new NotImplementedException();
        }
    }
}