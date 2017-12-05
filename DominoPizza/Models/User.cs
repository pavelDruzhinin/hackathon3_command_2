/*using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserFirstName { get; set; }
        public string UserPatronymic { get; set; }
        public string UserLastName { get; set; }

        public int UserRoleId { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        public ICollection<UserComment> UserComments { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<OnlineChatRow> OnlineChatRows { get; set; } //!!!!!!!!

        public void NewUser(string uFirstName, string uPatronymic, string uLastName) //, DateTime uBirthDate, Boolean uSex
        {
            UserFirstName = uFirstName;
            UserPatronymic = uPatronymic;
            UserLastName = uLastName;
 //           UserSex = uSex;
 //           UserBirthDate = uBirthDate;
        }

        public string UserFullName()
        {
            return $"{UserFirstName} {UserPatronymic} {UserLastName}";
        }

     
    }
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasRequired(x => x.UserRole).WithMany(x => x.Users).HasForeignKey(x => x.UserRoleId);
            HasMany(x => x.Tasks);
        }
    }
}*/