﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Qwerty.DAL.EF;

namespace Qwerty.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190615100416_ThirdMig")]
    partial class ThirdMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Qwerty.DAL.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Qwerty.DAL.Entities.Friend", b =>
                {
                    b.Property<string>("FriendId");

                    b.HasKey("FriendId");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("Qwerty.DAL.Entities.FriendshipRequest", b =>
                {
                    b.Property<string>("SenderUserId");

                    b.Property<string>("RecipientUserId");

                    b.Property<int>("Status");

                    b.Property<DateTime>("TimeSent");

                    b.HasKey("SenderUserId", "RecipientUserId");

                    b.HasIndex("RecipientUserId");

                    b.ToTable("FriendshipRequests");
                });

            modelBuilder.Entity("Qwerty.DAL.Entities.Message", b =>
                {
                    b.Property<int>("IdMessage")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAndTimeMessage");

                    b.Property<string>("IdRecipient");

                    b.Property<string>("IdSender");

                    b.Property<string>("TextMessage");

                    b.HasKey("IdMessage");

                    b.HasIndex("IdRecipient");

                    b.HasIndex("IdSender");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Qwerty.DAL.Entities.User", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Qwerty.DAL.Entities.UserFriends", b =>
                {
                    b.Property<string>("FriendId");

                    b.Property<string>("UserId");

                    b.HasKey("FriendId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFriends");
                });

            modelBuilder.Entity("Qwerty.DAL.Entities.UserProfile", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("AboutUrl");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Email");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("Surname");

                    b.HasKey("UserId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Qwerty.DAL.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Qwerty.DAL.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Qwerty.DAL.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Qwerty.DAL.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Qwerty.DAL.Entities.Friend", b =>
                {
                    b.HasOne("Qwerty.DAL.Entities.UserProfile", "UserProfile")
                        .WithOne("ProfileAsFriend")
                        .HasForeignKey("Qwerty.DAL.Entities.Friend", "FriendId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Qwerty.DAL.Entities.FriendshipRequest", b =>
                {
                    b.HasOne("Qwerty.DAL.Entities.User", "RecipientUser")
                        .WithMany("ReceiveFriendshipRequests")
                        .HasForeignKey("RecipientUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Qwerty.DAL.Entities.User", "SenderUser")
                        .WithMany("SendFriendshipRequests")
                        .HasForeignKey("SenderUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Qwerty.DAL.Entities.Message", b =>
                {
                    b.HasOne("Qwerty.DAL.Entities.User", "RecipientUser")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("IdRecipient")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Qwerty.DAL.Entities.User", "SenderUser")
                        .WithMany("SendMessages")
                        .HasForeignKey("IdSender")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Qwerty.DAL.Entities.User", b =>
                {
                    b.HasOne("Qwerty.DAL.Entities.ApplicationUser", "ApplicationUser")
                        .WithOne("User")
                        .HasForeignKey("Qwerty.DAL.Entities.User", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Qwerty.DAL.Entities.UserFriends", b =>
                {
                    b.HasOne("Qwerty.DAL.Entities.Friend", "Friend")
                        .WithMany("UserFriends")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Qwerty.DAL.Entities.User", "User")
                        .WithMany("UserFriends")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Qwerty.DAL.Entities.UserProfile", b =>
                {
                    b.HasOne("Qwerty.DAL.Entities.User", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("Qwerty.DAL.Entities.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
