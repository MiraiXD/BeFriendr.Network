using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using BeFriendr.Network.UserProfiles.Entities;

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
            public class IncorrectStatusValueException : Exception
            {
                public string Status { get; }
                public IncorrectStatusValueException(string status)
                {
                    Status = status;
                }
            }
            public class UnauthorizedReceiverException : Exception
            {
                public string LoggedInUserName { get; }
                public Relationship Relationship { get; }
                public RelationshipStatus ParsedStatus { get; }

                public UnauthorizedReceiverException(string loggedInUserName, Relationship relationship, RelationshipStatus parsedStatus)
                {
                    LoggedInUserName = loggedInUserName;
                    Relationship = relationship;
                    ParsedStatus = parsedStatus;
                }
            }
            public class UnauthorizedSenderException : Exception
            {
                public string LoggedInUserName { get; }
                public Relationship Relationship { get; }
                public RelationshipStatus ParsedStatus { get; }

                public UnauthorizedSenderException(string loggedInUserName, Relationship relationship, RelationshipStatus parsedStatus)
                {
                    LoggedInUserName = loggedInUserName;
                    Relationship = relationship;
                    ParsedStatus = parsedStatus;
                }
            }
            public class ActionNotAllowed : Exception
            {
                public Relationship Relationship { get; }
                public RelationshipStatus Status { get; }
                public ActionNotAllowed(Relationship relationship, RelationshipStatus status)
                {

                }
            }
        }
    }
}