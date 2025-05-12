namespace HR.LeaveManagement.Application.Common;

public interface IDispatcher
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
}
