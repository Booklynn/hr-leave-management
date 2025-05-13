using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IAppLogger<DeleteLeaveAllocationCommandHandler> logger) : IRequestHandler<DeleteLeaveAllocationCommand, int>
    {
        public async Task<int> Handle(DeleteLeaveAllocationCommand request)
        {
            var leaveAllocationToDelete = await leaveAllocationRepository.GetByIdAsync(request.Id);
            if (leaveAllocationToDelete == null)
            {
                logger.LogWarning("{@LeaveAllocation} - {@Id} was not found for deleting", nameof(LeaveAllocation), request.Id);
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);
            }

            await leaveAllocationRepository.DeleteAsync(leaveAllocationToDelete);
            logger.LogInformation("{@LeaveAllocation} - {@Id} was deleted successfully", nameof(LeaveAllocation), leaveAllocationToDelete.Id);

            return leaveAllocationToDelete.Id;
        }
    }
}
