using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class UserRole
    {
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }

        public ICollection<User> Users { get; set; }

    }

    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            HasMany(x => x.Users);
        }

    }
}