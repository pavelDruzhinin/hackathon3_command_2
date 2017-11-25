using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class Contacts
    {
        public int ContactsId { get; set; }
        public string ContactsAdress { get; set; }
        public DateTime ContactsDateLatestOrder { get; set; }

        
        public ICollection<Customer> CustomerId { get; set; } // несколько человек на адресе и городской телефон
    }
}