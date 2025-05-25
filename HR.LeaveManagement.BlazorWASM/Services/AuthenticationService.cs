using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorWASM.Contracts;
using HR.LeaveManagement.BlazorWASM.Models.Auth;
using HR.LeaveManagement.BlazorWASM.Providers;
using HR.LeaveManagement.BlazorWASM.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorWASM.Services;

public class AuthenticationService(
    IClient client,
    ILocalStorageService localStorageService,
    AuthenticationStateProvider authenticationStateProvider
    ) : BaseHttpService(client, localStorageService), IAuthenticationService
{
    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        try
        {
            var authenticationResponse = await _client.LoginAsync(new AuthRequest
            {
                Email = email,
                Password = password
            });

            if (string.IsNullOrEmpty(authenticationResponse.Token))
            {
                return false;
            }

            await _localStorageService.SetItemAsync("Token", authenticationResponse.Token);
            await ((APIAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return true;

        }
        catch (Exception) 
        {
            return false;
        }
    }

    public async Task Logout()
    {
        await ((APIAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
    }

    public async Task<bool> RegisterAsync(RegisterViewModel model)
    {
        var response = await _client.RegisterAsync(new RegistrationRequest
        {
           FirstName = model.FirstName,
           LastName = model.LastName,
           UserName = model.UserName,
           Email = model.Email,
           Password = model.Password
        });

        if (string.IsNullOrEmpty(response.UserId))
        {
            return false;
        }
        return true;
    }
}
