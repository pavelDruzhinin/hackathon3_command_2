using System;
using System.Data.Entity.ModelConfiguration;

namespace DominosPizza.Models
{
    public class FeedbackMail
    {
        
            public int FeedbackMailId { get; set; }
            public string Subject { get; set; }

            public string Body { get; set; }
            public DateTime MailDateCreate { get; set; }
            private string to = "admindominos@yandex.ru";
            public string To { get { return to; } set { to = value; } }

            public string FilePath { get; set; }
            public int CustomerId { get; set; }
            public Customer Customer { get; set; }
        }

        public class FeedbackMailMap : EntityTypeConfiguration<FeedbackMail>
        {
            public FeedbackMailMap()
            {
                HasRequired(x => x.Customer).WithMany(x => x.FeedBackMails).HasForeignKey(x => x.CustomerId);
            }
        }
    
}