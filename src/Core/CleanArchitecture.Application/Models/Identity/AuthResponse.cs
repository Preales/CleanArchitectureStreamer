namespace CleanArchitecture.Application.Models.Identity
{
    public record AuthResponse(string Id, string Username, string Email, string Token);
}