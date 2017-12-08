﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DominosPizza.Models;

namespace DominosPizza.Models
{
    public class DominosContext : DbContext
    {
        public DominosContext() : base("DominosContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DominosContext, DominoPizza.Migrations.Configuration>("DominosContext"));
        }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public DbSet<Product> Products { get; set; }
        //public DbSet<User> Customers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<TaskRow> TaskRows { get; set; }
        public DbSet<FeedbackMail> FeedBacks { get; set; }
        public DbSet<UserComment> UserComments { get; set; }

        public DbSet<OnlineChatMessage> OnlineChatMessages { get; set; }
        public DbSet<TaskStatusChange> TaskStatusChanges { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Configurations.Add(new TaskMap());
            //modelBuilder.Configurations.Add(new UserMap());
           // modelBuilder.Configurations.Add(new UserCommentMap());
            modelBuilder.Configurations.Add(new ContactMap());
            modelBuilder.Configurations.Add(new UserMap());
            //modelBuilder.Configurations.Add(new ProductMap());
            //modelBuilder.Configurations.Add(new TaskRowMap());
            //modelBuilder.Configurations.Add(new UserRoleMap());
            //modelBuilder.Configurations.Add(new OnlineChatMessageMap());

            base.OnModelCreating(modelBuilder);

        }
    }

    /* protected override void OnModelCreating(DbModelBuilder modelBuilder)
     {
         modelBuilder.Configurations.Add(new TaskMap());

         base.OnModelCreating(modelBuilder);

     }*/
}