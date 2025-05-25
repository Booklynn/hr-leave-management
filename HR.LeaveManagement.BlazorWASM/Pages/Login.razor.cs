using HR.LeaveManagement.BlazorWASM.Contracts;
using HR.LeaveManagement.BlazorWASM.Models.Auth;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorWASM.Pages;

public partial class Login
{
    public required LoginViewModel Model { get; set; }

    public required string Message { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IAuthenticationService AuthenticationService { get; set; }

    protected override void OnInitialized()
    {
        Model = new LoginViewModel();
    }

    protected async Task HandleLogin()
    {
        if (await AuthenticationService.AuthenticateAsync(Model.Email, Model.Password))
        {
            NavigationManager.NavigateTo("/");
        }
        Message = "Invalid username or password";
    }
}