using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace DominosPizza.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string CustomerFirstName { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string CustomerPatronymic { get; set; }
        public string CustomerLastName { get; set; }
        public DateTime CustomerBirthDate { get; set; }
        public Boolean CustomerSex { get; set; }
        public string CustomerPhone { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string CustomerEmail { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string CustomerPassword { get; set; }

        [Compare("CustomerPassword", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string CustomerPasswordConfirm { get; set; }

        public int CustomerRoleId { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<UserComment> UserComments { get; set; }
        public ICollection<FeedbackMail> FeedBackMails { get; set; }
    }

    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            HasMany(x => x.Contacts);
            HasMany(x => x.Tasks);
        }
    }
}