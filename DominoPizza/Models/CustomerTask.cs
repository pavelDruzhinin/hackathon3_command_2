using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using DominosPizza.Models;

namespace DominoPizza.Models
{
    public class CustomerTask
    {
        public int CustomerId { get; set; }
        public int TaskId { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }

    public class CustomerTaskMap : EntityTypeConfiguration<CustomerTask>
    {
        public CustomerTaskMap()
        {
            HasMany(x => x.Tasks);
            HasMany(x => x.Customers);
        }
    }
}