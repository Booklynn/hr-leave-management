namespace HR.LeaveManagement.Application.Exceptions;

public class NotFoundException(string name, object key) : Exception($"{name} ({key}) was not found");

public class InvalidMapRequestToEntity(string name, object key) : Exception($"{name} ({key}) was not found");
