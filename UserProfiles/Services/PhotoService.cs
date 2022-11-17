using BeFriendr.Network.UserProfiles.Helpers;
using BeFriendr.Network.UserProfiles.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using BeFriendr.Network.UserProfiles.Exceptions;

namespace BeFriendr.Network.UserProfiles.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> options)
        {
            var settings = options.Value;
            var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }
        public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (!file.ContentType.Contains("image")) throw new UserProfileExceptions.Photos.Cloudinary.IncorrectFileFormatException("Uploaded file must be an image!");
            if (file.Length <= 0) throw new UserProfileExceptions.Photos.Cloudinary.CorruptFileException("The file is corrupt (length = 0B)");

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream)
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null) throw new UserProfileExceptions.Photos.Cloudinary.FileUploadErrorException(uploadResult.Error.Message);

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicID)
        {
            var deletionParams = new DeletionParams(publicID);
            return await _cloudinary.DestroyAsync(deletionParams);
        }
    }
}