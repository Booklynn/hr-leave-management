namespace HR.LeaveManagement.Application.Models.Email;

public class EmailMessage
{
    public required string From { get; set; }
    public required string To { get; set; }
    public required string Subject { get; set; }
    public string? Body {  get; set; }
}
