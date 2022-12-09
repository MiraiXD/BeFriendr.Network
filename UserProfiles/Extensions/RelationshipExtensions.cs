using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.UserProfiles.Repositories;
using BeFriendr.Network.UserProfiles.Services;

namespace BeFriendr.Network.UserProfiles.Extensions
{
    public static class RelationshipExtensions
    {
        public static IServiceCollection AddRelationshipServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRelationshipRepository, RelationshipRepository>();
            services.AddScoped<IRelationshipService,RelationshipService>();
            return services;
        }
    }
}