namespace EBP.Infrastructure.Options
{
    public class JwtOptions
    {
        public required string Issuer { get; init; }
        public required string Audience { get; init; }
        public required string SecurityKey { get; init; }
        public int ExpiresHours { get; init; }
    }
}
