using EmailService.Contracts;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace EmailService.Smtp
{
    public class EmailService : IEmailService
    {
        private readonly SmtpOptions _smtpOptions;

        public EmailService(IOptions<SmtpOptions> smtpOptions)
        {
            _smtpOptions = smtpOptions.Value;
        }

        public async Task<bool> SendEmailAsync(EmailMessage emailMessage,CancellationToken cancellationToken)
        {
            var mimeMessage = emailMessage.ToMailKitMessage();
            using var smtpClient = new SmtpClient();
            try
            {
                await smtpClient.ConnectAsync(_smtpOptions.Smtp.Host, _smtpOptions.Smtp.Port, _smtpOptions.Smtp.UseSsl, cancellationToken);
                await smtpClient.AuthenticateAsync(_smtpOptions.Smtp.Username, _smtpOptions.Smtp.Password, cancellationToken);
                await smtpClient.SendAsync(mimeMessage, cancellationToken);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
