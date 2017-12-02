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
        public EmailResult SendEmail(FeedbackMail model)
        {
            To.Add(model.To);

            From = "customermail@yandex.ru"; //e-mail user 

            Subject = model.Subject;

            return Email("SendEmail", model);
        }
    }
}