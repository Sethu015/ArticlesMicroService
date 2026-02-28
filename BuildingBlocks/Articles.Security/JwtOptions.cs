namespace Articles.Security
{
    public class JwtOptions
    {
        public required string Issuer { get; init; }
        public required string Audience { get; init; }
        public required string Secret { get; init; }
        public required int ExpiryMinutes { get; init; }
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan ValidFor => TimeSpan.FromMinutes(ExpiryMinutes);
        public DateTime Expiration => IssuedAt.Add(ValidFor);
    }
}
