using BeFriendr.Common;
using BeFriendr.Network.Messages.Repositories;
using BeFriendr.Network.UserProfiles.Repositories;

namespace BeFriendr.Network
{
    public class UnitOfWork : BaseUnitOfWork<NetworkDbContext>
    {
        public IUserProfilesRepository UserProfilesRepository { get; }
        public IMessageRepository MessageRepository { get; }
        public IRelationshipRepository RelationshipRepository {get;}
        public UnitOfWork(NetworkDbContext dbContext, IUserProfilesRepository userProfilesRepository, IMessageRepository messageRepository, IRelationshipRepository relationshipRepository) : base(dbContext)
        {
            UserProfilesRepository = userProfilesRepository;
            MessageRepository = messageRepository;
            RelationshipRepository=relationshipRepository;
        }
    }
}