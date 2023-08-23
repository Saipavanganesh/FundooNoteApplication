using FundooSub.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FundooSub.Services
{
    public class UserRegistrationEmailSubscriber :  IConsumer<UserRegistrationMessage>
    {
        public async Task Consume(ConsumeContext<UserRegistrationMessage> context)
        {
            var userRegistrationMessage = context.Message;

            // Send a welcome email to the registered user using an email service
            await SendWelcomeEmail(userRegistrationMessage.Email);
        }

        private async Task SendWelcomeEmail(string email)
        {

            try
            {

                // Configure SMTP settings
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential("pavanganeshms@gmail.com", "awwhgrvzheqsdafw");
                    smtpClient.EnableSsl = true;

                    // Create the email message
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("pavanganeshms@gmail.com");
                        mailMessage.To.Add(email);
                        mailMessage.Subject = "Welcome to Our App!";
                        mailMessage.Body = "Thank you for registering. Welcome to our app!";
                        mailMessage.IsBodyHtml = true;

                        // Send the email
                        smtpClient.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
