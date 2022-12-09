using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFriendr.Network.UserProfiles.Exceptions
{
    public class RelationshipExceptions
    {
        public class Create
        {
            public class UserDoesNotExistException : Exception
            {
                public string UserName { get; }
                public UserDoesNotExistException(string userName) : base()
                {
                    UserName = userName;
                }
            }
        }
        public class SetStatus
        {
            public class NotFoundException : Exception
            {
                public string SenderUserName { get; }
                public string ReceiverUserName { get; }
                public NotFoundException(string senderUserName, string receiverUserName) : base()
                {
                    ReceiverUserName = receiverUserName;
                    SenderUserName = senderUserName;
                }
            }
            public class IncorrectStatusValue : Exception
            {
                public string Status { get; }
                public IncorrectStatusValue(string status)
                {
                    Status = status;
                }
            }
        }
    }
}