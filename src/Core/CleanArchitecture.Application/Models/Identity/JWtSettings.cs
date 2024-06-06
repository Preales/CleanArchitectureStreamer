namespace CleanArchitecture.Application.Models.Identity
{
    public record JWtSettings(string Key, string Issuer, string Audience, double DurationInMinute);
}