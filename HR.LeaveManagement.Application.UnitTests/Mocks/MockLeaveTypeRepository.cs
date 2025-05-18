using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks;

public class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository(List<LeaveType> leaveTypes)
    {
        var mockRepository = new Mock<ILeaveTypeRepository>();

        mockRepository.Setup(repository => repository.GetListAsync()).ReturnsAsync(leaveTypes);
        
        mockRepository.Setup(repository => repository.CreateAsync(It.IsAny<LeaveType>()))
            .Returns((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType);
                return Task.CompletedTask;
            });
        
        return mockRepository;
    }
}
