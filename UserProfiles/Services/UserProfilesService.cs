using AutoMapper;
using BeFriendr.Common;
using BeFriendr.Network.UserProfiles.Entities;
using BeFriendr.Network.UserProfiles.Exceptions;
using BeFriendr.Network.UserProfiles.Repositories;
using BeFriendr.Network.UserProfiles.Requests;
using Microsoft.EntityFrameworkCore;

namespace BeFriendr.Network.UserProfiles.Services
{
    public class UserProfilesService : IUserProfilesService
    {
        private readonly IMapper _mapper;
        private readonly IUserProfilesRepository _userProfilesRepository;
        public UserProfilesService(IUserProfilesRepository userProfilesRepository, IMapper mapper)
        {
            _userProfilesRepository = userProfilesRepository;
            _mapper = mapper;
        }
        public async Task<UserProfile> GetByUserNameAsync(string userName)
        {
            return await _userProfilesRepository
            .AsQueryable()
            .Where(profile => profile.UserName == userName)
            .Include(profile => profile.Photos)
            .Include(profile => profile.RelationshipsReceived)
            .ThenInclude(relationship=>relationship.SendingProfile)
            .ThenInclude(profile=>profile.Photos)
            .Include(profile => profile.RelationshipsSent)
            .ThenInclude(relationship=>relationship.ReceivingProfile)
            .ThenInclude(profile=>profile.Photos)
            .FirstOrDefaultAsync();
        }
        public async Task<UserProfile> GetAsync(GetProfileRequest request)
        {
            return await _userProfilesRepository
            .AsQueryable()
            .Where(x => x.UserName == request.UserName)
            .Include(x => x.Photos)
            .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<UserProfile>> GetManyAsync(GetManyProfilesRequest request)
        {
            var query = _userProfilesRepository.AsQueryable();

            if (!string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(x => x.UserName == request.UserName);
            }
            query = query.Include(x => x.Photos);

            return await PagedList<UserProfile>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
        public async Task<UserProfile> CreateAsync(CreateProfileRequest request)
        {
            var userProfile = await _userProfilesRepository.GetByUserNameAsync(request.UserName);
            if (userProfile != null) throw new UserProfileExceptions.Create.AlreadyExistsException($"User profile with username: {request.UserName} already exists! Cannot create a new one with the same user name");

            userProfile = _mapper.Map<UserProfile>(request);
            _userProfilesRepository.Insert(userProfile);
            return userProfile;
        }

        public async Task DeleteAsync(string userName, DeleteProfileRequest request)
        {
            var userProfile = await _userProfilesRepository.GetByUserNameAsync(userName);
            if (userProfile == null) throw new UserProfileExceptions.Delete.NotFoundException($"User profile with user name: {userName} does not exist. Cannot delete profile!");
            _userProfilesRepository.Delete(userProfile.ID);
        }
        public async Task<UserProfile> UpdateAsync(string userName, UpdateProfileRequest request)
        {
            var userProfile = await _userProfilesRepository.GetByUserNameAsync(userName);
            if (userProfile == null) throw new UserProfileExceptions.Update.NotFoundException($"User profile with user name: {userName} has not been found. Cannot update database!");
            if (await _userProfilesRepository.GetByUserNameAsync(request.UserName) != null) throw new UserProfileExceptions.Update.AlreadyExistsException($"User name: {request.UserName} is already taken ");

            userProfile = _mapper.Map<UpdateProfileRequest, UserProfile>(request, userProfile);
            _userProfilesRepository.Update(userProfile);

            return userProfile;
        }
        public Task AddPhotoAsync(UserProfile profile, Photo photo)
        {
            profile.Photos.Add(photo);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<UserProfile>> GetManyByUserNamesAsync(IEnumerable<string> userNames)
        {
            return await _userProfilesRepository.AsQueryable()
            .Where(profile => userNames.Contains(profile.UserName))
            .ToListAsync();
            // var queryable = _userProfilesRepository.AsQueryable()
            // .Join(userNames, profile => profile.UserName, userName=> userName, (profile, userName)=> profile);

            // return await queryable.ToListAsync();

        }
    }
}