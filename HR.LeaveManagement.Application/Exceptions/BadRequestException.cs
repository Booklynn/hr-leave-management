using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions;

public class BadRequestException : Exception
{
    public List<string> ValidationErrors { get; set; } = [];

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, ValidationResult result) : base(message)
    {
        ValidationErrors = [.. result.Errors.Select(error => error.ErrorMessage)];
    }
}
