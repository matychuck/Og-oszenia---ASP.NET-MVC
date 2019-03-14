using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace Ads
{
    public class Email
    {

        // wywołanie funkcji -> Email.SendEmail(parametry), nie trzeba robić jakiegoś new Email itp.
        public static void SendEmail(string subject, string body, string email)
        {
            var fromEmail = new MailAddress("ogloszenia.localhost@gmail.com","Serwis ogłoszeń");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "12qwaszx.";

            string myBody = body;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = myBody,
                IsBodyHtml = true
            })

                smtp.Send(message);
        }
    }
}