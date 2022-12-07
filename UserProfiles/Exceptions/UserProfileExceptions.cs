using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BeFriendr.Network.UserProfiles.Exceptions
{
    public class UserProfileExceptions
    {
        public class Get
        {
            public class NotFoundException : Exception
            {
                public NotFoundException(string message) : base(message)
                {

                }
            }
        }
        public class Delete
        {
            public class NotFoundException : Exception
            {
                public NotFoundException(string message) : base(message)
                {

                }
            }
        }
        public class Update
        {
            public class NotFoundException : Exception
            {
                public NotFoundException(string message) : base(message)
                {

                }
            }
            public class AlreadyExistsException : Exception
            {
                public AlreadyExistsException(string message) : base(message)
                {
                }
            }
        }
        public class Create
        {
            public class AlreadyExistsException : Exception
            {
                public AlreadyExistsException(string message) : base(message)
                {

                }
            }
        }
        public class Photos
        {
            public class Cloudinary
            {
                public class CorruptFileException : Exception
                {
                    public CorruptFileException(string message) : base(message) { }
                }
                public class FileUploadErrorException : Exception
                {
                    public FileUploadErrorException(string message) : base(message) { }
                }
                public class IncorrectFileFormatException : Exception
                {
                    public IncorrectFileFormatException(string message) : base(message) { }
                }
            }
        }
    }
}