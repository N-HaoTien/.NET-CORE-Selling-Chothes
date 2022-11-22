using Microsoft.AspNetCore.Identity.UI.Services;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Mail;
using System.Net.Mime;
using NuGet.Protocol;

namespace ClothingShopping
{
    public class SendGridEmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("nht.it19@gmail.com");
            msg.To.Add(new MailAddress(toEmail));
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.Body = message;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            //msg.Attachments
            //smtp-mail.outlook.com
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("nht.it19@gmail.com", "anhyeuem3");
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
    }
}
