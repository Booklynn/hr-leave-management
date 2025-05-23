namespace HR.LeaveManagement.Application.Models.Email;

public class EmailSettings
{
    public required string APIKey { get; set; }
    public required string FromAddress { get; set; }
    public string? FromName { get; set; }
}
