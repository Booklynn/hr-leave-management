﻿using HR.LeaveManagement.Application.Common;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public record UpdateLeaveTypeCommand(int Id, string Name, int DefaultDays) : IRequest<int>;
