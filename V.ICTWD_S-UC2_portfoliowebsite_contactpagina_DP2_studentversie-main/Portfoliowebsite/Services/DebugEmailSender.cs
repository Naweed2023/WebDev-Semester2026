using System.Diagnostics;

namespace Portfoliowebsite.Services
{
    public class DebugEmailSender : IEmailSender
    {
        public Task SendAsync(string Name, string Email, string Subject, string Message)
        {
            Debug.WriteLine("---- Contact form (DEV) ----");
            Debug.WriteLine($"Name: {Name}");
            Debug.WriteLine($"Email: {Email}");
            Debug.WriteLine($"Subject: {Subject}");
            Debug.WriteLine($"Message: {Message}");
            Debug.WriteLine("---------------------------");
            return Task.CompletedTask;
        }
    }
}