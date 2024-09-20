using System.ComponentModel.DataAnnotations;

namespace authentication_security.Core.Application.ViewModels;

public class LogInRequestViewModel
{
    [Required(ErrorMessage = "Debe ingresar un nombre de usuario o un correo electrónico.")]
    public string UserNameOrEmail { get; set; }

    [Required(ErrorMessage = "Debe ingresar una contraseña")]
    public string Password { get; set; }
}

