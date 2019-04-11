﻿using Microsoft.AspNet.Identity.EntityFramework;
using Qwerty.DAL.Entities;
using System.Data.Entity;

namespace Qwerty.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string ConnectionString) : base(ConnectionString)
        {
            Database.SetInitializer(new QwertyDbInitializer());
        }
        public DbSet<User> QUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<FriendshipRequest> Requests { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<UserFriends> UserFriends { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region UserSettings
            modelBuilder.Entity<User>().HasKey(x => x.UserId);
            modelBuilder.Entity<User>().Property(x => x.UserId)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<ApplicationUser>().HasRequired(x => x.User).WithRequiredPrincipal(x => x.ApplicationUser);
            #endregion
            #region FriendSettings
            modelBuilder.Entity<Friend>().HasKey(x => x.FriendId);
            modelBuilder.Entity<Friend>().HasRequired(x => x.UserProfile).WithOptional(x => x.ProfileAsFriend);
            #endregion
            #region Messages
            modelBuilder.Entity<Message>().HasKey(x => x.IdMessage);
            modelBuilder.Entity<Message>().Property(x => x.IdMessage).
                HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Message>().HasRequired(x => x.SenderUser).WithMany(x => x.SendMessages).HasForeignKey(x => x.IdSender).WillCascadeOnDelete(false);
            modelBuilder.Entity<Message>().HasRequired(x => x.RecipientUser).WithMany(x => x.RecivedMessages).HasForeignKey(x => x.IdRecipient).WillCascadeOnDelete(false);
            #endregion
            #region FriendshipRequest
            modelBuilder.Entity<FriendshipRequest>().HasKey(x => new { x.SenderUserId, x.RecipientUserId });
            modelBuilder.Entity<FriendshipRequest>().HasRequired(x => x.RecipientUser).WithMany(x => x.ReciveFriendshipRequests)
                .HasForeignKey(x => x.RecipientUserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<FriendshipRequest>().HasRequired(x => x.SenderUser).WithMany(x => x.SendFriendshipRequests)
                .HasForeignKey(x => x.SenderUserId).WillCascadeOnDelete(false);
            #endregion
            #region UserProfileSettings
            modelBuilder.Entity<UserProfile>().HasKey(x => x.UserId);
            modelBuilder.Entity<UserProfile>().HasRequired(x => x.User).WithRequiredPrincipal(x => x.UserProfile);
            #endregion
            #region UserFriendsSettings
            modelBuilder.Entity<UserFriends>().HasKey(x => new { x.FriendId, x.UserId });
            modelBuilder.Entity<UserFriends>().HasRequired(x => x.User).WithMany(x => x.UserFriends);
            modelBuilder.Entity<UserFriends>().HasRequired(x => x.Friend).WithMany(x => x.UserFriends);
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
