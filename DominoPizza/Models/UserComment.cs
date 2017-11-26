using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class UserComment
    {
        public int UserCommentId { get; set; }
        public string UserCommentText { get; set; }
        public DateTime UserCommentDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int TaskId { get; set; }
        public User Task { get; set; }

    }

    public class UserCommentMap : EntityTypeConfiguration<UserComment>
    {
        public UserCommentMap()
        {
            HasRequired(x => x.User).WithMany(x => x.UserComments).HasForeignKey(x => x.UserId);
            HasRequired(x => x.Task).WithMany(x => x.UserComments).HasForeignKey(x => x.TaskId);

        }
    }
}