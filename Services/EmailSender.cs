using Simplifly.Interfaces;
using System.Net.Mail;
using System.Net;

namespace Simplifly.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("tejask21@outlook.com", "79105Tejas")
            };

            return client.SendMailAsync(
                new MailMessage(from: "tejask21@outlook.com",
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}
