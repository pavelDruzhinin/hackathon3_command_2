using DominosPizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class TaskDetails 
    {
        public int TaskId { get; set; }
        public double TotalSum { get; set; }
        public string Status { get; set; }
        public DateTime DateTime { get; set; } //!!! время, когда нужно доставить заказ
        public int PayMethod { get; set; }
        public int RoleId { get; set; }
        public string ClientFullName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientAddress { get; set; }
        public int userrole { get; set; }
        public bool inWorkFlag { get; set; }
        public List<User> staff = new List<User>();
        public List<UserComment> comments = new List<UserComment>();
        public List<TaskRow> tasklist = new List<TaskRow>();
        public List<TaskStatusChange> changes = new List<TaskStatusChange>();
        public List<Product> products = new List<Product>();
        public List<UserRole> roles = new List<UserRole>();
    }
}