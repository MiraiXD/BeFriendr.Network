using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BeFriendr.Network.Messages.Repositories;
using BeFriendr.Network.Messages.Services;

namespace BeFriendr.Network.Messages.Extensions
{
    public static class MessageExtensions
    {
        public static IServiceCollection AddMessageServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}