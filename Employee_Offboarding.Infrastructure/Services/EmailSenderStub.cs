using Employee_Offboarding.Application.Abstractions.Services;
using Microsoft.Extensions.Logging;

namespace Employee_Offboarding.Infrastructure.Services
{
    public class EmailSenderStub : IEmailSender
    {
        private readonly ILogger<EmailSenderStub> _logger;
        public EmailSenderStub(ILogger<EmailSenderStub> logger) => _logger = logger;
        public Task SendAsync(string to, string subject, string htmlBody, CancellationToken ct = default)
        {
            _logger.LogInformation("Email -> {to} | Subject -> {subject} | Body -> {htmlBody}", to, subject, htmlBody);
            return Task.CompletedTask;
        }
    }
}
