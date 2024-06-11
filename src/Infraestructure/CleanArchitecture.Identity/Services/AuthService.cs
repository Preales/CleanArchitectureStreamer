using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JWtSettings _jWtSettings;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<JWtSettings> jWtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jWtSettings = jWtSettings.Value;
    }

    public async Task<AuthResponse> LoginAsync(AuthRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new Exception($"el usuario con email {request.Email} no existe");
        }

        var resultado = await _signInManager.PasswordSignInAsync(user.UserName!, request.Password, false, lockoutOnFailure: false);

        if (!resultado.Succeeded)
        {
            throw new Exception("Las credenciales son incorrectas");
        }
        var token = await GetToken(user);
        var authRespone = new AuthResponse(user.Id, token, user.Email!, user.UserName!);
        return authRespone;
    }

    public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
    {
        await ValidationUser(request);

        var user = new ApplicationUser
        {
            Email = request.Email,
            Nombre = request.Nombre,
            Apellidos = request.Apellidos,
            UserName = request.Username,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await AddToRole(user);
            var token = await GetToken(user);
            return new RegistrationResponse(user.Id, user.UserName!, user.Email!, token);
        }

        throw new Exception($"{result.Errors}");
    }

    private async Task<string> GetToken(ApplicationUser user)
    {
        var jwtSecurityToken = await GenerateToken(user);
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.UserName !),
            new Claim(JwtRegisteredClaimNames.Email,user.Email !),
            new Claim(CustomClaimTypes.Uid,user.Id)
        }.Union(userClaims).Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jWtSettings.Issuer,
                audience: _jWtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jWtSettings.DurationInMinute),
                signingCredentials: signingCredentials
            );

        return jwtSecurityToken;
    }

    private async Task AddToRole(ApplicationUser user)
        => await _userManager.AddToRoleAsync(user, "Support");

    private async Task ValidationUser(RegistrationRequest request)
    {
        var exisitngUser = await _userManager.FindByNameAsync(request.Username);
        if (exisitngUser != null)
            throw new Exception("El username ya fue tomado por otra cuenta");

        var existingEmail = await _userManager.FindByEmailAsync(request.Email);
        if (existingEmail != null)
            throw new Exception("El email ya existe");
    }
}