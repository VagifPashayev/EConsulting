using Microsoft.Extensions.Configuration;
using EConsult.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using EConsult.Services.Abstracts;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace EConsult.Services.Concretes
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async void SendEmail(string subject, string content, string recipient)
        {
            await SendEmailAsync(new MessageDto
            {
                Subject = subject,
                Content = content,
                Recipients = new List<string> { recipient }
            });
        }

        public async void SendEmail(string subject, string content, params string[] recipients)
        {
            await SendEmailAsync(new MessageDto
            {
                Subject = subject,
                Content = content,
                Recipients = recipients.ToList()
            });
        }

        public async void SendEmail(MessageDto message)
        {
            await SendEmailAsync(message);
        }

        public async Task SendEmailAsync(MessageDto messageDto)
        {
            //var emailSettings = _configuration.GetSection("EmailSettings");
            //string apiKey = emailSettings["ApiKey"];
            //var client = new SendGridClient(apiKey);

            //var msg = new SendGridMessage()
            //{
            //    From = new EmailAddress("ceka592@yandex.com", "Medicoz"),
            //    Subject = $"Medicoz",
            //    PlainTextContent = $"salam",
            //};
            //msg.AddTo(new EmailAddress($"{"vagiffp@code.edu.az"}"));

            //var response = await client.SendEmailAsync(msg);
            //var msg = CreateEmailMessage(messageDto, emailSettings);
            //var response = await client.SendEmailAsync(msg);

            //Console.WriteLine(response.StatusCode);
            //if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
            //{
            //    throw new Exception();
            //}
            try
            {
                var emailSettings = _configuration.GetSection("EmailSettings");
                string apiKey = emailSettings["ApiKey"];
                var client = new SendGridClient(apiKey);
                var msg = CreateEmailMessage(messageDto, emailSettings);
                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private SendGridMessage CreateEmailMessage(MessageDto messageDto, IConfigurationSection emailSettings)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(emailSettings["Email"], emailSettings["Username"]),
                Subject = messageDto.Subject,
                PlainTextContent = messageDto.Content,
            };

            foreach (var recipient in messageDto.Recipients)
            {
                msg.AddTo(new EmailAddress(recipient));
            }

            return msg;
        }
    }
}
