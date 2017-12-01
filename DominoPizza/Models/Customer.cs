using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerPatronymic { get; set; }
        public string CustomerLastName { get; set; }
        public DateTime CustomerBirthDate { get; set; }
        public Boolean CustomerSex { get; set; }
        public string CustomerPhone{get;set;}
        public string CustomerEmail { get; set; }


        public ICollection<Contact> Contacts{ get; set; }
        public ICollection<OnlineChatRow> OnlineChatRows { get; set; } //!!!!!!!!


        public void NewCustomer(string cFirstName,  string cPatronymicName, string cLastName, DateTime cBirthDate, Boolean cSex, string cPhone, string cEmail)
        {
            CustomerFirstName = cFirstName;
            CustomerPatronymic = cPatronymicName;
            CustomerLastName = cLastName;
            CustomerBirthDate = cBirthDate;
            CustomerSex = cSex;
            CustomerPhone = cPhone;
            CustomerEmail = cEmail;
        }
    }

    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            HasMany(x => x.Contacts);

        }
    }
}