using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public double TaskTotalSumm { get; set; }
        public string TaskStatus { get; set; }
        public DateTime TaskDate { get; set; }
        public ICollection<Products> ProductId { get; set; }
        public ICollection<Comments> CommentId { get; set; }
        public Contacts ContactId { get; set; }
        public ICollection<User> UserId { get; set; }
    }
}