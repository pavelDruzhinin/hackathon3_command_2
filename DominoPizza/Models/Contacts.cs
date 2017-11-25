using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class Contacts
    {
        public int ContactsId { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public ICollection<Customer> CustomerId { get; set; } // несколько человек на адресе и городской телефон
    }
}