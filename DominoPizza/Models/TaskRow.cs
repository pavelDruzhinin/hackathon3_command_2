using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DominosPizza.Models;
using System.Data.Entity.ModelConfiguration;

namespace DominosPizza.Models
{
    public class TaskRow
    {
        public int TaskRowId { get; set; }
        public int Quantity { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
         
    }

    //public class TaskRowMap : EntityTypeConfiguration<TaskRow>
    //{
    //    public TaskRowMap()
    //    {
    //        HasRequired(x => x.Task).WithMany(x => x.TaskRows).HasForeignKey(x => x.TaskId);
    //        HasRequired(x => x.Product).WithMany(x => x.TaskRows).HasForeignKey(x => x.ProductId);
    //    }
    //}
}