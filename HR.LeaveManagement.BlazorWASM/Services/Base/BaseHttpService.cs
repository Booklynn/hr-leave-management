using System.Net;

namespace HR.LeaveManagement.BlazorWASM.Services.Base;

public class BaseHttpService(IClient client)
{
    protected IClient _client = client;

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
}
