using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.BlazorWASM.Models.Auth;

public class LoginViewModel
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
