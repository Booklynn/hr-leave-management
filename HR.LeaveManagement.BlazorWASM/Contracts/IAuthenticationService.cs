using HR.LeaveManagement.BlazorWASM.Models.Auth;

namespace HR.LeaveManagement.BlazorWASM.Contracts;

public interface IAuthenticationService
{
    Task<bool> AuthenticateAsync(string email, string password);
    Task<bool> RegisterAsync(RegisterViewModel registerViewModel);
    Task Logout();
}
