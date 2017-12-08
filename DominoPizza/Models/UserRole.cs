using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class UserRole
    {
        public int UserRoleId { get; set; } //1 - КЛИЕНТ
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

    }

    //public class UserRoleMap : EntityTypeConfiguration<UserRole>
    //{
    //    public UserRoleMap()
    //    {
    //        HasMany(x => x.Users);
    //    }

    //}
}