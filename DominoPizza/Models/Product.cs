using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Type { get; set; } // 1-пицца
        public string ImageLink { get; set; }

        public ICollection<TaskRow> TaskRows { get; set; }

    }

    //public class ProductMap:EntityTypeConfiguration<Product>

    //{
    //    public ProductMap()
    //    {
    //        HasMany(x => x.TaskRows);
    //    }
    //}
}