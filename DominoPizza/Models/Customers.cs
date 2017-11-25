using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class Customer
    {
        private int CustomerId { get; set; }
        private string CustomerFirstName { get; set; }
        private string CustomerLastName { get; set; }
        private string CustomerPatronymic { get; set; }
        private DateTime CustomerBirthDate { get; set; }
        private Boolean CustomerSex { get; set; }
        private ICollection<Contacts> ContactsId { get; set; }

        //public void NewCustomer(string Name, string PatronymicName, string SurName, DateTime BirthDate)
        //{
        //    customerName = Name;
        //    customerPatronymicName = PatronymicName;
        //    customerSurName = SurName;
        //    customerBirthDate = BirthDate;
        //    customerCreateDate = DateTime.Now;
        //}
    }
}