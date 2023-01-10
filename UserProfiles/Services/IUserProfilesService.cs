using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Common;
using BeFriendr.Network.UserProfiles.Entities;
using BeFriendr.Network.UserProfiles.Requests;

namespace BeFriendr.Network.UserProfiles.Services
{
    public interface IUserProfilesService
    {
        Task<UserProfile> GetByUserNameAsync(string userName);
        Task<IEnumerable<UserProfile>> GetManyByUserNamesAsync(IEnumerable<string> userNames);
        Task<UserProfile> GetAsync(GetProfileRequest request);
        Task<IEnumerable<UserProfile>> GetManyAsync(GetManyProfilesRequest request);
        Task<UserProfile> CreateAsync(CreateProfileRequest request);
        Task<UserProfile> UpdateAsync(string userName, UpdateProfileRequest request);
        Task DeleteAsync(string userName, DeleteProfileRequest request);
        Task AddPhotoAsync(UserProfile profile, Photo photo);
    }
}