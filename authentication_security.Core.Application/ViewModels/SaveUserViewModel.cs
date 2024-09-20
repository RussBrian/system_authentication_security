
using System.ComponentModel.DataAnnotations;

namespace authentication_security.Core.Application.ViewModels;
public class SaveUserViewModel
{
    [Required(ErrorMessage = "Debe ingresar un nombre de usuario.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Debe ingresar un nombre.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Debe ingresar un apellido.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Debe ingresar una correo electrónico.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Debe ingresar una contraseña")]
    public string Password { get; set; } 

    [Required(ErrorMessage = "Debe ingresar una contraseña")]
    [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden.")]
    public string ConfirmPassword { get; set; } 
}