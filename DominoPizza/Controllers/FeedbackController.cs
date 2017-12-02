using ActionMailer.Net.Mvc;
using DominoPizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DominoPizza.Controllers
{
    public class FeedbackController : MailerBase
    {
        public EmailResult SendEmail(FeedbackMail model)
        {
            To.Add(model.To);

         //   From = model.From; //e-mail user 

            Subject = model.Subject;

            return Email("SendEmail", model);
        }
    }
}