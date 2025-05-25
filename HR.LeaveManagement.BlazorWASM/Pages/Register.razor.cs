using HR.LeaveManagement.BlazorWASM.Contracts;
using HR.LeaveManagement.BlazorWASM.Models.Auth;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorWASM.Pages;

public partial class Register
{
    public required RegisterViewModel Model { get; set; }
    public required string Message { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IAuthenticationService AuthenticationService { get; set; }

    protected override void OnInitialized()
    {
        Model = new RegisterViewModel();
    }

    protected async Task HandleRegister()
    {
        var result = await AuthenticationService.RegisterAsync(Model);

        if (result)
        {
            NavigationManager.NavigateTo("/");
        }
        Message = "Something went wrong, please try again.";
    }
}