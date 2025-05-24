using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Identity.Services;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    public async Task<Employee> GetEmployee(string userId)
    {
        var employee = await userManager.FindByIdAsync(userId);
        return new Employee
        {
            Email = employee?.Email,
            Id = employee?.Id,
            FirstName = employee?.FirstName,
            LastName = employee?.LastName,
        };
    }

    public async Task<IReadOnlyList<Employee>> GetEmployees()
    {
        var employees = await userManager.GetUsersInRoleAsync("Employee");
        return [.. employees.Select(employee => new Employee
        {
            Email = employee?.Email,
            Id = employee?.Id,
            FirstName = employee?.FirstName,
            LastName = employee?.LastName,
        })];
    }
}
