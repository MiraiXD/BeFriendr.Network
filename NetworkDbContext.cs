using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.Messages.Entities;
using BeFriendr.Network.UserProfiles.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeFriendr.Network
{
    public class NetworkDbContext : DbContext
    {
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public NetworkDbContext(DbContextOptions<NetworkDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserProfile>()
            .HasMany(u => u.Photos)
            .WithOne(p => p.UserProfile);

            builder.Entity<Message>()
            .HasOne(message=>message.SenderProfile)
            .WithMany(sender => sender.MessagesSent)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
            .HasOne(message=>message.RecipientProfile)
            .WithMany(recipient => recipient.MessagesReceived)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}