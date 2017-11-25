using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class OrderTable
    {
        public int OrderTableId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int ProductQuantity { get; set; }

        public int PreSum()
        {
            return ProductPrice * ProductQuantity;
        }
    }
}