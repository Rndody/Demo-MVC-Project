using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MVC_Project_Presentation_Layer.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public async Task SendAsync(string from, string recipients, string subject, string body)
        {
            var senderEmail = configuration["EmailSettings:SenderEmail"];
            var senderPassword = configuration["EmailSettings:SenderPassword"];

            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress(from);
            emailMessage.To.Add(recipients);
            emailMessage.Subject = subject;
            emailMessage.Body = $"<html> <body>  {body}   </body> </html>";

            emailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient(configuration["EmailSettings:SmtpClientServer"], int.Parse(configuration["EmailSettings:SmtpClientPort"]   )       )//simple mail transfere protocol
            {
                Credentials=new NetworkCredential(senderEmail,senderPassword),
                EnableSsl = true
            
            };
            await smtpClient.SendMailAsync(emailMessage);

        }
    }
}
