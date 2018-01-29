using DevHub.DAL.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DevHub.BLL.Methods
{
    public class EmailMethod
    {
        private readonly IOptions<AppSettingModel> _options;

        public EmailMethod(IOptions<AppSettingModel> options)
        {
            _options = options;
        }

        public async Task SendEmail(EmailParameters model, string username)
        {
            var htmlFilePath = "./Templates/" + model.Template + ".html";
            var builder = new BodyBuilder
            {
                HtmlBody = File.ReadAllText(htmlFilePath)

                    .Replace("^Fullname^", (model.Firstname + " " + model.Lastname))
                    .Replace("^Date^", model.Date)
                    .Replace("^Time^", model.Time)
                    .Replace("^Email^", model.Email)
                    .Replace("^Space^", model.Space)
                    .Replace("^Message^", model.Message)
                    .Replace("^ContactNumber^", model.ContactNumber)
                    .Replace("^Bill^", model.Bill)
                    .Replace("^Period^", model.Period)
                    .Replace("^Rate^", model.Rate)
                    .Replace("^Duration^", model.Duration)
                    .Replace("^Number^", model.GuestCount.ToString())
                    .Replace("^RoomType^", model.RoomType)
                    .Replace("^ReferenceNumber^", model.ReferenceNumber)
                    .Replace("^ConfirmedBy^", username)
            };

            var emailMessage = new MimeMessage
            {
                From = {
                    new MailboxAddress("Dev Partners",  _options.Value.SenderEmail)
                },
                To = {
                    new MailboxAddress("Dev Partners", model.IsAdmin ? model.Recipient : model.Recipient)
                },
                Subject = model.Subject,
                Body = builder.ToMessageBody()
            };

            await SendEmailAsync(emailMessage);

        }


        private async Task SendEmailAsync(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Timeout = 900000;
                await client.ConnectAsync("smtp.gmail.com", 587, false).ConfigureAwait(false);

                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_options.Value.SenderEmail, _options.Value.Password);

                await client.SendAsync(message).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);

            }
        }


    }
}
