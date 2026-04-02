// TransportRouteApi/DTOs/LoginDto.cs
using System.ComponentModel.DataAnnotations;

namespace TransportRouteApi.DTOs;

public class LoginUserDto
{
    // Let's make it either the username or email for flexibility, but we won't enforce strict rules here.
    [Required(ErrorMessage = "Username or email is required.")]
    public string Username { get; set; } = string.Empty;

    // Notice there are NO Regex or MinLength rules here!
    // We just need to capture the exact string the user typed.
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = string.Empty;
}