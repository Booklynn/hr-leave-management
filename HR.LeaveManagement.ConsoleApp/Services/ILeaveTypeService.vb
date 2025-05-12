Public Interface ILeaveTypeService
    Function DisplayLeaveTypesAsync() As Task
    Function DisplayLeaveTypeByIdAsync(id As Integer) As Task
End Interface
