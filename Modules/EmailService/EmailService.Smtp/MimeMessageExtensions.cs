using EmailService.Contracts;
using MimeKit;

namespace EmailService.Smtp
{
    internal static class MimeMessageExtensions
    {
        public static MailboxAddress ToMailBoxAddress(this EmailAddress emailAddress) => new MailboxAddress(emailAddress.Name, emailAddress.Address);

        public static MimeMessage ToMailKitMessage(this EmailMessage emailMessage)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(emailMessage.From.ToMailBoxAddress());
            mimeMessage.To.AddRange(emailMessage.To.Select(x => x.ToMailBoxAddress()));
            mimeMessage.Subject = emailMessage.Subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = emailMessage.Content.Value
            };
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            return mimeMessage;
        }
    }
}
