using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorWASM.Contracts;
using HR.LeaveManagement.BlazorWASM.Services.Base;

namespace HR.LeaveManagement.BlazorWASM.Services;

public class LeaveRequestService(IClient client, ILocalStorageService localStorageService) : BaseHttpService(client, localStorageService), ILeaveRequestService
{
}
