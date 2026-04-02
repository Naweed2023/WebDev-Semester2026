using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Portfoliowebsite.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public SmtpEmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAsync(string Name, string Email, string Subject, string Message)
        {
            // FR02: configurable recipient
            var recipient = _config["ContactForm:RecipientEmail"];
            if (string.IsNullOrWhiteSpace(recipient))
                throw new InvalidOperationException("ContactForm:RecipientEmail ontbreekt in appsettings.json");

            // read SMTP settings from config (instead of hardcoding)
            var host = _config["Smtp:Host"] ?? "smtp.mailtrap.io";
            var port = int.TryParse(_config["Smtp:Port"], out var p) ? p : 2525;
            var enableSsl = bool.TryParse(_config["Smtp:EnableSsl"], out var ssl) && ssl;

            var username = _config["Smtp:Username"] ?? "";
            var password = _config["Smtp:Password"] ?? "";

            // OLD codes
            // var smtp = new SmtpClient("smtp.mailtrap.io", 2525)
            // {
            //     EnableSsl = false,
            //     Credentials = new NetworkCredential("", "")
            // };

            using var smtp = new SmtpClient(host, port)
            {
                EnableSsl = enableSsl,
                Credentials = new NetworkCredential(username, password)
            };

            // configurable "From" nice to have 
            var fromEmail = _config["ContactForm:FromEmail"] ?? "noreply@example.com";
            var fromName = _config["ContactForm:FromName"] ?? "Website";

            // Basic hardening: prevent subject header injection
            Subject = (Subject ?? "").Replace("\r", "").Replace("\n", "");

            using var mail = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = $"Contact: {Subject}",
                Body = $"Naam: {Name}\nEmail: {Email}\nBericht:\n{Message}"
            };

            // OLD code:
            // mail.To.Add("contact@example.com");

            mail.To.Add(recipient);

            await smtp.SendMailAsync(mail);
        }
    }
}