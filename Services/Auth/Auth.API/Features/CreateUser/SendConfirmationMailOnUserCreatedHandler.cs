using Auth.Domain.Events;
using Auth.Domain.Users;
using EmailService.Contracts;
using FastEndpoints;
using Microsoft.Extensions.Options;
using Blocks.AspNetCore.Extensions;
using Flurl;

namespace Auth.API.Features.CreateUser
{
    public class SendConfirmationMailOnUserCreatedHandler : IEventHandler<UserCreated>
    {
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SmtpOptions _options;

        public SendConfirmationMailOnUserCreatedHandler(IEmailService emailService,IOptions<SmtpOptions> options,IHttpContextAccessor httpContextAccessor)
        {
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _options = options.Value;
        }

        public async Task HandleAsync(UserCreated eventModel, CancellationToken ct)
        {
            var uri = _httpContextAccessor.HttpContext.Request?.BaseUrl()
                .AppendPathSegment("password")
                .AppendQueryParam(new { eventModel.ResetPasswordToken });
            var emailMessage = BuildConfirmationEmail(eventModel.User, "", _options.EmailFromAddress);
            await _emailService.SendEmailAsync(emailMessage,ct);
        }

        public EmailMessage BuildConfirmationEmail(User user,string resetLink,string fromEmailAddress)
        {
            const string confirmationMail =
                "Dear {0} <br/> An account has been created for you. <br/> Please set the password using following url <br/> {1}";

            return new EmailMessage(
                "Account has been Created - Set Password",
                new EmailContent(EmailContentType.Html, string.Format(confirmationMail, user.FullName, resetLink)),
                new EmailAddress("Article", fromEmailAddress),
                new List<EmailAddress> { new EmailAddress(user.FullName, user.Email) }
                );
        }
    }
}
