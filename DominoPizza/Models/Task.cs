﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using DominoPizza.Models;

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
        public  int CustomerId { get; set; }

       // private Dictionary<int, DateTime> taskStatusChange = new Dictionary<int, DateTime>();//<ИД пользователя сменившего статус, время>

        public ICollection<UserComment> UserComments { get; set; }

        public ICollection<StatusHistory> History { get; set; }

        public int ContactId { get; set; }
        public Contact Contact { get; set; }

        public ICollection<Customer> Customers { get; set; }

        public ICollection<TaskRow> TaskRows { get; set; }
        //public ICollection<CustomerTask> CustomerTasks { get; set; }

        
               
    }
    

    public enum Status {processed, kitchen, fordelivery, delivery, done, canceled}

    public class TaskMap : EntityTypeConfiguration<Task>
    {
        public TaskMap()
        {
            HasRequired(x => x.Contact).WithMany(x => x.Tasks).HasForeignKey(x => x.ContactId);
            HasMany(x => x.Customers);
            HasMany(x => x.History);

        }
    }
}