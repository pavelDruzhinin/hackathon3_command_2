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
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int UserId { get; set; } 

        public ICollection<User> Users{ get; set; } // несколько человек на адресе 
        public ICollection<Task> Tasks { get; set; }
    }

    public class ContactMap: EntityTypeConfiguration<Contact>
    {
        public ContactMap()
        {
            HasMany(x => x.Users);
        }
    }
}