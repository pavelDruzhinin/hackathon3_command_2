using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;


namespace DominosPizza.Models
{
    public class StatusHistory
    {
        public int StatusHistoryId { get; set; }
        public string StatusChangedTo { get; set; }
        public DateTime StatusChangeTime { get; set; }
        public Task ForTask { get; set; }
        public Customer DominosUser { get; set; }
    }
    public class StatusHistoryMap : EntityTypeConfiguration<StatusHistory>
    {
        public StatusHistoryMap()
        {
            HasRequired(x => x.ForTask);
            HasRequired(x => x.DominosUser);
        }
    }
}
