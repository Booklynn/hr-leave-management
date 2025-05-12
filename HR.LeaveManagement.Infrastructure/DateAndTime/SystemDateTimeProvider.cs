using HR.LeaveManagement.Application.Contracts.DateAndTime;

namespace HR.LeaveManagement.Infrastructure.DateAndTime;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}
