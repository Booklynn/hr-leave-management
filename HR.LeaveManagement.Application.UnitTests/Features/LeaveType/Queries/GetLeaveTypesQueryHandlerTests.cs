using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetListLeaveTypes;
using HR.LeaveManagement.Application.Mappings.LeaveType;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveType.Queries;

public class GetLeaveTypesQueryHandlerTests
{
    private readonly Mock<IAppLogger<GetLeaveTypesQueryHandler>> _logger;

    public GetLeaveTypesQueryHandlerTests()
    {
        _logger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
    }

    [Fact]
    public async Task Get_ListLeaveType_ReturnsLeaveTypes()
    {
        var leaveTypes = new List<Domain.LeaveType>
        {
            new() {
                Id = 1,
                Name = "Test Vacation Leave",
                DefaultDays = 7
            },
            new() {
                Id = 2,
                Name = "Test Sick Leave",
                DefaultDays = 3
            }
        };

        var mockLeaveTypeRepository = MockLeaveTypeRepository.GetMockLeaveTypeRepository(leaveTypes);
        var mapper = new QueryLeaveTypeMapper();
        var handler = new GetLeaveTypesQueryHandler(mockLeaveTypeRepository.Object, mapper, _logger.Object);
        
        var result = await handler.Handle(new GetLeaveTypesQuery());

        result.ShouldBeAssignableTo<IReadOnlyList<LeaveTypeDTO>>();
        result.Count.ShouldBe(2);
        result[0].Name.ShouldBe("Test Vacation Leave");
        result[1].Name.ShouldBe("Test Sick Leave");

        mockLeaveTypeRepository.Verify(repository => repository.GetListAsync(), Times.Once);
        _logger.Verify(logger => logger.LogInformation(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
    }

    [Fact]
    public async Task Get_EmptyListLeaveType_ReturnsEmptyList()
    {
        var mockLeaveTypeRepository = MockLeaveTypeRepository.GetMockLeaveTypeRepository([]);
        var mapper = new QueryLeaveTypeMapper();
        var handler = new GetLeaveTypesQueryHandler(mockLeaveTypeRepository.Object, mapper, _logger.Object);

        var result = await handler.Handle(new GetLeaveTypesQuery());

        result.ShouldBeAssignableTo<IReadOnlyList<LeaveTypeDTO>>();
        result.ShouldBeEmpty();
        result.Count.ShouldBe(0);
    }
}
