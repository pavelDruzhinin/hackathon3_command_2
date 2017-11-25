using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DominosPizza.Models;


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
}