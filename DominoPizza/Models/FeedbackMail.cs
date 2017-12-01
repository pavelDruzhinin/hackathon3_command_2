using DominosPizza.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DominoPizza.Models
{
    public class FeedbackMail
    {
        public int FeedbackMailId { get; set; }
        public string Subject { get; set; }

        public string Body { get; set; }
        public DateTime MailDateCreate { get; set; }
        private string to = "alu-sia@yandex.ru";
        public string To { get { return to; } set { to = value; } }

        public int UserId { get; set; }
        public User User { get; set; }
    }

    public class FeedbackMailMap : EntityTypeConfiguration<FeedbackMail>
    {
        public FeedbackMailMap()
        {
            HasRequired(x => x.User).WithMany(x => x.FeedBackMails).HasForeignKey(x => x.UserId);
        }
    }

}