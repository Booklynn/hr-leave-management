namespace HR.LeaveManagement.Application.Features.LeaveType.DTOs;

public class LeaveTypeDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}
