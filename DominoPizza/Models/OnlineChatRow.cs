using DominosPizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;

namespace DominosPizza.Models
{
    public class OnlineChatRow
    {
        public int OnlineChatRowId { get; set; }

        public int CustomerId { get; set; }      //!!!     0 если незарегистрированный
        public Customer Customer { get; set; }   //!!!
        public int UserId { get; set; }          //!!!
        public User User { get; set; }           //!!!

        public string OnlineChatUniqueId { get; set; }
        public DateTime MessageDateTime { get; set; }
        public bool Assigned { get; set; }  // если правда, то чат взят в работу конкретным менеджером
        public bool MessageByUser { get; set; } // если правда, то сообщение от менеджера, ложь - от клиента
        public string MessageText { get; set; }
        public bool MessageIsNew { get; set; }

    }
    public class OnlineChatRowMap : EntityTypeConfiguration<OnlineChatRow>
    {
        public OnlineChatRowMap()
        {
            HasRequired(x => x.User).WithMany(x => x.OnlineChatRows).HasForeignKey(x => x.UserId);
            HasRequired(x => x.Customer).WithMany(x => x.OnlineChatRows).HasForeignKey(x => x.CustomerId);
        }
    }
}