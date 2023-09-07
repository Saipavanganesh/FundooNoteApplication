using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class MessageService
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;
        private readonly IConfiguration _configuration;
        public MessageService(IConfiguration configuration, ServiceBusClient client, ServiceBusSender sender)
        {
            this._configuration = configuration;
            this._client = client;
            this._sender = sender;
            /*string connectionString = _configuration.GetConnectionString("AzureServiceBusConnectionString");
            string queueName = _configuration["AzureServiceBusQueueName"];
            client = new ServiceBusClient(connectionString);
            sender = _client.CreateSender(queueName);*/
        }
        public void SendMessage2Queue(string email, string token)
        {
            string messageBody = $"Token : {token}";
            ServiceBusMessage message = new ServiceBusMessage();
            message.Subject = "Reset Token";
            message.To = email;
            message.Body = BinaryData.FromString(messageBody);
            _sender.SendMessageAsync(message).Wait();
            ReciveAndSendMails();
        }
        public void ReciveAndSendMails()
        {
            string connectionString = _configuration["AzureServiceBus:AzureServiceBusConnectionString"];
            string queueName = "passwordresetqueue";
            ServiceBusProcessor processor = _client.CreateProcessor(queueName, new ServiceBusProcessorOptions());
            processor.ProcessMessageAsync += async args =>
            {
                string messageBody = Encoding.UTF8.GetString(args.Message.Body);
                string email = args.Message.Subject;
                sendEmail(email, messageBody);
                await args.CompleteMessageAsync(args.Message);
            };
            processor.ProcessErrorAsync += args =>
            {
                return Task.CompletedTask;
            };
            processor.StartProcessingAsync().Wait();
        }
        public void sendEmail(string email, string messageBody)
        {
            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("pavanganeshms@gmail.com", "awwhgrvzheqsdafw"),
                EnableSsl = true
            };
            MailMessage mailMessage = new MailMessage();    
            mailMessage.From = new MailAddress(email);
            mailMessage.To.Add(email);
            mailMessage.Subject = "Subject of the Email";
            mailMessage.Body = messageBody;
            try
            {
                SMTP.Send(mailMessage);
                Console.WriteLine("Email Sent Succesfully");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
