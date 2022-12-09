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

            modelBuilder.Entity("BeFriendr.Network.Messages.Entities.Message", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("DateRead")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateSent")
                        .HasColumnType("datetime2");

                    b.Property<int>("RecipientID")
                        .HasColumnType("int");

                    b.Property<int?>("RecipientProfileID")
                        .HasColumnType("int");

                    b.Property<string>("RecipientUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SenderID")
                        .HasColumnType("int");

                    b.Property<int?>("SenderProfileID")
                        .HasColumnType("int");

                    b.Property<string>("SenderUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("RecipientProfileID");

                    b.HasIndex("SenderProfileID");

                    b.ToTable("Messages");
                });

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

            modelBuilder.Entity("BeFriendr.Network.UserProfiles.Entities.Relationship", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("BlockedByProfileID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateSent")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReceivingProfileID")
                        .HasColumnType("int");

                    b.Property<int>("SendingProfileID")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ReceivingProfileID");

                    b.HasIndex("SendingProfileID");

                    b.ToTable("Relationships");
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

            modelBuilder.Entity("BeFriendr.Network.Messages.Entities.Message", b =>
                {
                    b.HasOne("BeFriendr.Network.UserProfiles.Entities.UserProfile", "RecipientProfile")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("RecipientProfileID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BeFriendr.Network.UserProfiles.Entities.UserProfile", "SenderProfile")
                        .WithMany("MessagesSent")
                        .HasForeignKey("SenderProfileID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("RecipientProfile");

                    b.Navigation("SenderProfile");
                });

            modelBuilder.Entity("BeFriendr.Network.UserProfiles.Entities.Photo", b =>
                {
                    b.HasOne("BeFriendr.Network.UserProfiles.Entities.UserProfile", "UserProfile")
                        .WithMany("Photos")
                        .HasForeignKey("UserProfileID");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("BeFriendr.Network.UserProfiles.Entities.Relationship", b =>
                {
                    b.HasOne("BeFriendr.Network.UserProfiles.Entities.UserProfile", "ReceivingProfile")
                        .WithMany("RelationshipsReceived")
                        .HasForeignKey("ReceivingProfileID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BeFriendr.Network.UserProfiles.Entities.UserProfile", "SendingProfile")
                        .WithMany("RelationshipsSent")
                        .HasForeignKey("SendingProfileID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ReceivingProfile");

                    b.Navigation("SendingProfile");
                });

            modelBuilder.Entity("BeFriendr.Network.UserProfiles.Entities.UserProfile", b =>
                {
                    b.Navigation("MessagesReceived");

                    b.Navigation("MessagesSent");

                    b.Navigation("Photos");

                    b.Navigation("RelationshipsReceived");

                    b.Navigation("RelationshipsSent");
                });
#pragma warning restore 612, 618
        }
    }
}
