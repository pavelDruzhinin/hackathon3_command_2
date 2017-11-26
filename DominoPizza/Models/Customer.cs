using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        private string CustomerFirstName { get; set; }
        private string CustomerPatronymic { get; set; }
        private string CustomerLastName { get; set; }
        private DateTime CustomerBirthDate { get; set; }
        private Boolean CustomerSex { get; set; }
        private string CustomerPhone{get;set;}
        private string CustomerEmail { get; set; }


        private ICollection<Contact> Contacts{ get; set; }

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
}