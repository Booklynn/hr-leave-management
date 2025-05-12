namespace HR.LeaveManagement.Application.Contracts.DateAndTime;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}
