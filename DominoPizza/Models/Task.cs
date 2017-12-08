using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public double TotalSum { get; set; }
        public string Status { get; set; }        
        public DateTime DateTime { get; set; } //!!! время, когда нужно доставить заказ
        public int PayMethod { get; set; }
        public int? UserComment { get; set; } 
        public int RoleId { get; set; } 

        public ICollection<UserComment> UserComments { get; set; }

        public int ContactId { get; set; }
        public Contact Contact { get; set; }

        public ICollection<TaskStatusChange> TaskStatusChange { get; set; }

        public ICollection<TaskRow> TaskRows { get; set; }

    }

    public enum Status {processed, kitchen, delivery, done, canceled}

    //public class TaskMap : EntityTypeConfiguration<Task>
    //{
    //    public TaskMap()
    //    {
    //        HasRequired(x => x.Contact).WithMany(x => x.Tasks).HasForeignKey(x => x.ContactId);
    //        HasMany(x => x.Users);
    //        
    //    }
    //}
}