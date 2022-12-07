using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Common;
using BeFriendr.Network.Messages.Entities;

namespace BeFriendr.Network.Messages.Repositories
{
    public interface IMessageRepository : ICrudRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessagesBetweenProfiles(int currentProfileID, int otherProfileID);
    }
}