namespace EmailService.Contracts
{
    public enum EmailContentType
    {
        Text,
        Html
    }
    public record EmailContent(EmailContentType ContentType,string Value);
    public record EmailAddress(string Name,string Address);
    public record EmailMessage(string Subject,EmailContent Content,EmailAddress From, List<EmailAddress> To);
}
