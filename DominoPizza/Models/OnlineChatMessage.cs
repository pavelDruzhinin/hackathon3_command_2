using DominosPizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;

namespace DominosPizza.Models
{
    public class OnlineChatMessage 
    {
        public int OnlineChatMessageId { get; set; }            //OnlineChatRow renamed

       /* public int CustomerId { get; set; }*/      //     0 если незарегистрированный
        //public Customer Customer { get; set; } 
        
        public int UserId { get; set; }          
        public User User { get; set; }
        public int ManagerId { get; set; } 
        public string OnlineChatUniqueId { get; set; }
        public DateTime DateTime { get; set; }
        public bool Assigned { get; set; }  // если правда, то чат взят в работу конкретным менеджером
        public bool IsByManager { get; set; } // если правда, то сообщение от менеджера, ложь - от клиента
        public string Text { get; set; }
        public bool IsNew { get; set; }

    }
    //public class OnlineChatMessageMap : EntityTypeConfiguration<OnlineChatMessage>
    //{
    //    public OnlineChatMessageMap()
    //    {
    //        HasRequired(x => x.User).WithMany(x => x.OnlineChatMessages).HasForeignKey(x => x.UserId);
    //        //HasRequired(x => x.Customer).WithMany(x => x.OnlineChatRows).HasForeignKey(x => x.CustomerId); 
    //    }
    //}
}