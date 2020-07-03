using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DecrypteService
{
    public class MailSender
    {
        private static MailSender instance;

        public static MailSender GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new MailSender();
                return instance;
            }
        }

        public void SendMail(string to, string body)
        {
            if(to == string.Empty)
            {
                return;
            }
            MailAddress toAddress = new MailAddress(to);
            MailAddress from = new MailAddress("clairont51@gmail.com");
            const string pass = "Deling51";
            const string subject = "Decrypt Service: Un résultat a été trouvé.";
            

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from.Address, pass)
            };

            using (MailMessage message = new MailMessage(from, toAddress)
            {
                Subject = subject,
                Body = body
            })

            smtp.Send(message);
        }
    }
}
