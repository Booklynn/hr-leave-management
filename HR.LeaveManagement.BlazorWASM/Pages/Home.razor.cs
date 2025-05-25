using HR.LeaveManagement.BlazorWASM.Contracts;
using HR.LeaveManagement.BlazorWASM.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorWASM.Pages;

public partial class Home
{
    [Inject]
    public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    public required IAuthenticationService AuthenticationService { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ((APIAuthenticationStateProvider)AuthenticationStateProvider)
            .GetAuthenticationStateAsync();
    }

    protected async Task LogoutAsync()
    {
        await AuthenticationService.Logout();
        NavigationManager.NavigateTo("/");
    }

    protected void GoToRegister()
    {
        NavigationManager.NavigateTo("/register");
    }
    protected void GoToLogin()
    {
        NavigationManager.NavigateTo("/login");
    }
}