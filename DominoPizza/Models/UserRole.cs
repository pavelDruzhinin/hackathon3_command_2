using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class UserRole
    {
        public int RoleId { get; set; }
        // Названия ролей 1 - менеджер, 2 - повар, 3 - курьер (можно добавить в dbSetInitialize для тестов)
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }

    }
}