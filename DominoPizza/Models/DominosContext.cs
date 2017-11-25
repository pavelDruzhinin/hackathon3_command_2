using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class DominosContext : DbContext
    {
        public DbSet<UserRoles> UserRolesDbSet { get; set; }
        public DbSet<Users> UsersDbSet { get; set; }
        public DbSet<Tasks> TasksDbSet { get; set; }
        public DbSet<TaskComments> TaskCommentsDbSet { get; set; }
        public DbSet<Products> ProductsDbSet { get; set; }
        public DbSet<Customer> CustomersDbSet { get; set; }
        public DbSet<Contacts> ContactsDbSet { get; set; }
        public DbSet<TaskList> TaskListsDbSet { get; set; }
        
    }
}