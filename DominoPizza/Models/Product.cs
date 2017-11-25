﻿using System;
using System.Collections.Generic;
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
        public int ProductType { get; set; }

        public ICollection<TaskRow> TaskRows { get; set; }



    }
}