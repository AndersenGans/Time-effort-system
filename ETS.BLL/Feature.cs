using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETS.BLL
{
    public class Feature
    {
        public static string SetPass(int x)
        {
            string pass = "";
            var r = new Random();
            while (pass.Length < x)
            {
                Char c = (char)r.Next(33, 125);
                if (Char.IsLetterOrDigit(c))
                    pass += c;
            }
            return pass;
        }

        public static void SendEmail(string email, string subject, string context)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("effort.time.tracking.system@gmail.com", "etsstudents603");
            MailMessage message = new MailMessage();
            message.From = new MailAddress("effort.time.tracking.system@gmail.com");
            message.To.Add(new MailAddress(email));
            message.Subject = subject;
            message.Body = context;
            client.Send(message);
        }
    }
}
