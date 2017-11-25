using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class TaskComments

    {
        public int TaskCommentsId { get; set; } 
        public int TaskId { get; set; }
        public int UserId { get; set; }

        private DateTime commentTime;

        public DateTime GetCommentTime()
        {
            return commentTime;
        }

        public void SetCommentTime(DateTime value)
        {
            commentTime = value;
        }

        public string CommentText { get; set; }
        public string UserName { get; set; }
    }
}