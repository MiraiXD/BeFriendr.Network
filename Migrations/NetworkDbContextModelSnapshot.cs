﻿// <auto-generated />
using System;
using BeFriendr.Network;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BeFriendr.Network.Migrations
{
    [DbContext(typeof(NetworkDbContext))]
    partial class NetworkDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BeFriendr.Network.UserProfiles.Entities.Photo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<int>("ProfileID")
                        .HasColumnType("int");

                    b.Property<string>("PublicID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserProfileID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserProfileID");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("BeFriendr.Network.UserProfiles.Entities.UserProfile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("ID");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("BeFriendr.Network.UserProfiles.Entities.Photo", b =>
                {
                    b.HasOne("BeFriendr.Network.UserProfiles.Entities.UserProfile", "UserProfile")
                        .WithMany("Photos")
                        .HasForeignKey("UserProfileID");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("BeFriendr.Network.UserProfiles.Entities.UserProfile", b =>
                {
                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}