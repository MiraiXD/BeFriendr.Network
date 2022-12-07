using System.Reflection;
using BeFriendr.Network.UserProfiles.Helpers;
using BeFriendr.Network.UserProfiles.Repositories;
using BeFriendr.Network.UserProfiles.Services;

namespace BeFriendr.Network.UserProfiles.Extensions
{
    public static class UserProfileExtensions
    {
        public static IServiceCollection AddProfileServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));            
            services.AddScoped<IUserProfilesRepository, UserProfilesRepository>();
            services.AddScoped<IUserProfilesService, UserProfilesService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}