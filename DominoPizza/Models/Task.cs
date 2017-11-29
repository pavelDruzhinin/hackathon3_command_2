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
        public double TaskTotalSum { get; set; }
        public string TaskStatus { get; set; }
        public DateTime TaskDate { get; set; }
        public int TaskPayMethod { get; set; }
        public string TaskCustomerComment { get; set; }

       // private Dictionary<int, DateTime> taskStatusChange = new Dictionary<int, DateTime>();//<ИД пользователя сменившего статус, время>

        public ICollection<UserComment> UserComments { get; set; }

        public int ContactId { get; set; }
        public Contact Contact { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<TaskRow> TaskRows { get; set; }


    }

    public enum Status {processed, kitchen, delivery, done, canceled}

    public class TaskMap : EntityTypeConfiguration<Task>
    {
        public TaskMap()
        {
            HasRequired(x => x.Contact).WithMany(x => x.Tasks).HasForeignKey(x => x.ContactId);
            HasMany(x => x.Users);

        }
    }
}