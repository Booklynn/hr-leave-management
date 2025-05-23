using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HR.LeaveManagement.Infrastructure.EmailServices;

public class EmailSender(IOptions<EmailSettings> emailSettings) : IEmailSender
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;

    public async Task<bool> SendEmail(EmailMessage email)
    {
        var sendGridClient = new SendGridClient(_emailSettings.APIKey);
        
        var from = new EmailAddress
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName
        };
        var to = new EmailAddress(email.To);
        var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
        
        var response = await sendGridClient.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}
