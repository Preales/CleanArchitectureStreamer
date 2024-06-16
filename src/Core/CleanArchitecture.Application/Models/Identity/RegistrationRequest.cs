namespace CleanArchitecture.Application.Models.Identity
{
    public record RegistrationRequest(string Nombre, string Apellidos, string Email, string Username, string Password);
}