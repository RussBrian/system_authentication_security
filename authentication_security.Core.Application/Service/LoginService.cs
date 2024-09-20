using authentication_security.Core.Application.Common;
using authentication_security.Core.Application.Helper;
using authentication_security.Core.Application.Interfaces;
using authentication_security.Core.Application.ViewModels;
using authentication_security.Core.Domain.Entities;
using authentication_security.Core.Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace authentication_security.Core.Application.Service;

public class LoginService
    (IUserRepository userRepository,
    ITokenRepository tokenRepository,
    IOptions<JWTSettings> settings)
    : ILoginService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenRepository _tokenRepository = tokenRepository;
    private readonly JWTSettings _jwtSettings = settings.Value;

    public async Task<Result<UserViewModel>> AuthenticateUser(LogInRequestViewModel requestVm)
    {        
        User? user = await _userRepository.GetUserByEmail(requestVm.UserNameOrEmail) ??
             await _userRepository.GetUserByUserName(requestVm.UserNameOrEmail);

        if (user == null)
        {
            return Result<UserViewModel>.Failure("Usuario no encontrado.");
        }
        string verifyPassword = EncryptHash256.EncryptPassword(requestVm.Password);

        if (user.Password != verifyPassword)
        {
            return Result<UserViewModel>.Failure("Contraseña incorrecta.");
        }

        UserToken? isUserValid = await _tokenRepository.GetByUserId(user.Id);

        if(isUserValid != null)
        {
            if (isUserValid.IsValid!)
            return Result<UserViewModel>.Failure("Deben autenticarse nuevamente.");
        }

        var token = GenerateJwtToken(user);

        await AddUserTokenInDB(token, user.Id);

        UserViewModel vm = new()
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email
        };

        return Result<UserViewModel>.Success(vm);
    }

    public async Task InvalidateTokenForUser(string userId) => await _tokenRepository.InvalidateUserToken(userId);


    /// <summary>
    /// Metodo que registra el token perteneciente al usuario una vez este se registra. 
    /// <param name="userName">Nombre de usuario ingresado</param>
    /// <returns>Devuelve true si el userName cumple con la expresión regular, del caso contrario devuelve false.</returns>
    private async Task<Result> AddUserTokenInDB(string token, string userId)
    {
        UserToken user = new()
        {
            Id = Guid.NewGuid().ToString(),
            Token = token,
            UserId = userId
        };
        await _tokenRepository.AddUserToken(user);
        return Result.Success();
    }


    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.UserName),
            new (ClaimTypes.Name, user.UserName),
            new (ClaimTypes.Email, user.Email)
        };

        // Clave
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
        var symmetricSecurityKey = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        // Crear el token JWT
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

}
