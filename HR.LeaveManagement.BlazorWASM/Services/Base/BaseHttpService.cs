using Blazored.LocalStorage;
using System.Net;
using System.Net.Http.Headers;

namespace HR.LeaveManagement.BlazorWASM.Services.Base;

public class BaseHttpService(IClient client, ILocalStorageService localStorageService)
{
    protected IClient _client = client;
    protected readonly ILocalStorageService _localStorageService = localStorageService;

    protected static Response<Guid> ConvertAPIExceptions<Guid>(ApiException ex)
    {
        if (ex.StatusCode == (int)HttpStatusCode.BadRequest)
        {
            return new Response<Guid>
            {
                Message = "Invalid data",
                ValidatioErrors = ex.Response,
                Success = false
            };
        }

        if (ex.StatusCode == (int)HttpStatusCode.NotFound)
        {
            return new Response<Guid>
            {
                Message = "The record was not found.",
                Success = false
            };
        }

        return new Response<Guid>
        {
            Message = "Something went wrong, please try again.",
            Success = false
        };
    }

    protected async Task AddBearerToken()
    {
        if (await _localStorageService.ContainKeyAsync("Token"))
            _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _localStorageService.GetItemAsync<string>("Token"));
    }
}
