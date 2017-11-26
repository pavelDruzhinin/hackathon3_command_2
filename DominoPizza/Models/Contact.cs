using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string ContactAddress { get; set; }
        public DateTime ContactDateLatestOrder { get; set; }

        
        public ICollection<Customer> Customers{ get; set; } // несколько человек на адресе 
        public ICollection<Task> Tasks { get; set; }

    }

    public class ContactMap: EntityTypeConfiguration<Contact>
    {
        public ContactMap()
        {
            HasMany(x => x.Customers);
        }

    }
}