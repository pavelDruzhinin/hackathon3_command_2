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
        public string ProductName { get; set; }
        public double ProductWeight { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public int ProductType { get; set; } // 1-пицца
        public string ImageLink { get; set; }

        public ICollection<TaskRow> TaskRows { get; set; }



    }

    public class ProductMap:EntityTypeConfiguration<Product>

    {
        public ProductMap()
        {
            HasMany(x => x.TaskRows);
        }
    }
}