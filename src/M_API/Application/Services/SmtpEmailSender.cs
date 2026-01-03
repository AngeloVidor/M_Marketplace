using Application.Services;
using M_API.Application.DTOs;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpSettings _settings;

        public SmtpEmailSender(SmtpSettings settings)
        {
            _settings = settings;
        }

        public async Task SendAsync(EmailMessage message)
        {
            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = _settings.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_settings.From),
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };

            mail.To.Add(message.To);

            await client.SendMailAsync(mail);
        }
    }

    public class SmtpSettings
    {
        public string Host { get; set; } = "";
        public int Port { get; set; } = 587;
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string From { get; set; } = "";
        public bool EnableSsl { get; set; } = true;
    }
}