using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers;

public class AccountController : ControllerBase
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
        => Ok(await _authService.LoginAsync(request));

    [HttpPost("Register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        => Ok(await (_authService.RegisterAsync(request)));
}
