using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DominosPizza.Models;

namespace DominoPizza.Models
{
    public class LoginModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string LastName { get; set; }
    }
}