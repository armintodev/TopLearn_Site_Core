using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace TopLearn.Core.Senders
{
    public class SendEmail
    {
        public static void Sendemail(string to,string subject,string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("workatarmin@gmail.com","تاپ لرن");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            //Attachment attachment = new Attachment("D:/textfile.txt");
            //mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential("mytestformycode@gmail.com","hiS&29Do25c#");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

        }
    }
}