using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPatronymic { get; set; }
        public Boolean UserSex { get; set; } // 0 - man or 1 - women
        public DateTime UserBirthDay { get; set; }
        public Roles UserRoleId { get; set; }
        public ICollection<Contacts> UserContactsId { get; set; }
    }
}