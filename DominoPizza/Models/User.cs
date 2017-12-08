using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace DominosPizza.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public Boolean Sex { get; set; }
        public string Phone { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string PasswordConfirm { get; set; }
       
        public ICollection<OnlineChatMessage> OnlineChatMessages { get; set; } //!!!!!!!!

        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }

        public ICollection<TaskStatusChange> TaskStatusChanges { get; set; }
        public ICollection<Contact> Contacts { get; set; }
       // public ICollection<Task> Tasks { get; set; }
        public ICollection<UserComment> UserComments { get; set; }
        public ICollection<FeedbackMail> FeedBackMails { get; set; }



    }

    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasMany(x => x.Contacts);
            //HasMany(x => x.Tasks);
            //HasRequired(x => x.UserRoles);
        }
    }
}