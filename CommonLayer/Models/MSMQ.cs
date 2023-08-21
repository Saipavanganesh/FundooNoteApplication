using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Models
{
    public class MSMQ
    {
        MessageQueue MessageQ = new MessageQueue();
        public void SendData2Queue(string Token)
        {
            MessageQ.Path = @".\private$\Bills";
            if (!MessageQueue.Exists(MessageQ.Path))
            {
                MessageQueue.Create(MessageQ.Path);
            }
            MessageQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            MessageQ.ReceiveCompleted += MessageQ_ReceiveCompleted;
            MessageQ.Send(Token);
            MessageQ.BeginReceive();
            MessageQ.Close();
        }
        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = MessageQ.EndReceive(e.AsyncResult);
                string Body = msg.Body.ToString();
                string Subject = "FundooNote Reset Link";
                var SMTP = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("pavanganeshms@gmail.com", "awwhgrvzheqsdafw"),
                    EnableSsl = true
                };
                SMTP.Send("pavanganeshms@gmail.com", "pavanganeshms@gmail.com", Subject, Body);
                MessageQ.BeginReceive();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
