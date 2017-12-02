using ActionMailer.Net.Mvc;
using DominosPizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DominosPizza.Controllers
{
    public class FeedbackController : MailerBase
    {
        public EmailResult SendEmail(FeedbackMail feedbackMail)
        {
            DominosContext db = new DominosContext();

            To.Add(feedbackMail.To);

            From = feedbackMail.From; //e-mail user 

            Subject = feedbackMail.Subject;

            return Email("Email", feedbackMail);
        }
    }
}