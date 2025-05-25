using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HR.LeaveManagement.BlazorWASM.Providers;

public class APIAuthenticationStateProvider(
    ILocalStorageService localStorageService, 
    JwtSecurityTokenHandler jwtSecurityTokenHandler) 
    : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        var isTokenPresent = await localStorageService.ContainKeyAsync("Token");
        if (!isTokenPresent)
        {
            return new AuthenticationState(user);
        }

        var savedToken = await localStorageService.GetItemAsync<string>("Token");
        var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
        if (tokenContent.ValidTo < DateTime.UtcNow)
        {
            await localStorageService.RemoveItemAsync("Token");
            return new AuthenticationState(user);
        }

        var claims = await GetClaims();
        user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        return new AuthenticationState(user);
    }

    public async Task LoggedIn()
    {
        var claims = await GetClaims();
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        var authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task LoggedOut()
    {
        await localStorageService.RemoveItemAsync("Token");
        var nobody = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(nobody));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task<IReadOnlyList<Claim>> GetClaims()
    {
        var savedToken = await localStorageService.GetItemAsync<string>("Token");
        var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
        var claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        return claims;
    }
}
