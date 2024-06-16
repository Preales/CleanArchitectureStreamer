using CleanArchitecture.Application.Models.Identity;

namespace CleanArchitecture.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(AuthRequest request);
    Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
}
