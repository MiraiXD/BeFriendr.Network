using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFriendr.Network.UserProfiles.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeFriendr.Network
{
    public class NetworkDbContext : DbContext
    {
        public DbSet<UserProfile> Profiles { get; set; }
        public NetworkDbContext(DbContextOptions<NetworkDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserProfile>()
            .HasMany(u => u.Photos)
            .WithOne(p => p.UserProfile);
        }
    }
}