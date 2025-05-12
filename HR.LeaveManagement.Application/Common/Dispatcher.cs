using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Application.Common;

public class Dispatcher(IServiceProvider serviceProvider) : IDispatcher
{
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));

        dynamic handler = serviceProvider.GetRequiredService(handlerType);
        return await handler.Handle((dynamic)request);
    }
}
