namespace CleanArchitecture.Application.Models.Identity
{
    public record RegistrationResponse(string UserId, string Username, string Email, string Token);
}