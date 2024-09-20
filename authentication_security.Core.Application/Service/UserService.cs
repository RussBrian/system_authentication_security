using authentication_security.Core.Application.Helper;
using authentication_security.Core.Application.Interfaces;
using authentication_security.Core.Application.ViewModels;
using authentication_security.Core.Application.Common;
using authentication_security.Core.Domain.Entities;
using Microsoft.Extensions.Options;
using authentication_security.Core.Domain.Settings;
using System.Text.RegularExpressions;

namespace authentication_security.Core.Application.Service;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> AddUser(SaveUserViewModel userVm)
    {
        if (!IsValidUserName(userVm.UserName))
        {
            return Result.Failure("El nombre de usuario no es válido.");
        }

        if (!IsValidPassword(userVm.Password))
        {
            return Result.Failure("La contraseña no es válida.");
        }

        User? userByEmailTask = await _userRepository.GetUserByEmail(userVm.Email);       

        if (userByEmailTask != null)
        {
            return Result.Failure("El correo electrónico ya está registrado.");
        }

        User? userByUserNameTask = await _userRepository.GetUserByUserName(userVm.UserName);
        if (userByUserNameTask != null)
        {
            return Result.Failure("El nombre de usuario ya está registrado.");
        }

        User user = new()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = userVm.UserName,
            Name = userVm.Name,
            LastName = userVm.LastName,
            Email = userVm.LastName,
            EmailConfirmed = true,
            Password =  EncryptHash256.EncryptPassword(userVm.Password),
        };

        await _userRepository.InsertUserAsync(user);

        return Result.Success();
    }       

    public async Task<UserViewModel> GetUser(string userId)        
    {
        User? user = await _userRepository.GetUserById(userId);
        return new UserViewModel()
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email
        };

    }

    #region Validation for user
    /// <summary>
    /// Validar nombre de usuario para que no contenga caracteres especiales, tenga al menos tres  caracteres alfanuméricos y que inicie con una letra.
    /// </summary>
    /// <param name="userName">Nombre de usuario ingresado</param>
    /// <returns>Devuelve true si el userName cumple con la expresión regular, del caso contrario devuelve false.</returns>
    private static bool IsValidUserName(string userName)
    {
        string userNamePattern = @"^[A-Za-z][A-Za-z0-9]{2,}$";
        return Regex.IsMatch(userName, userNamePattern);
    }

    /// <summary>
    /// Validar contraseña para que contenga al menoss una mayúscula, una minúscula, un numero, un carácter especia
    /// y debe tener un mínimo de 8 caracteres.
    /// </summary>
    /// <param name="password">Contraseña ingresada</param>
    /// <returns>Devuelve true si la contraseña es cumple con la expresión regular, del caso contrario devuelve false.</returns>
    private static bool IsValidPassword(string password)
    {
        string passwordPattern = @"^(?=.*[A-Z])(?=.*\d).{8,}$";
        return Regex.IsMatch(password, passwordPattern);
    }
    #endregion
}

