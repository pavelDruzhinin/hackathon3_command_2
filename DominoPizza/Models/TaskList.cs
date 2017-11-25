using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class TaskList
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}