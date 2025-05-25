using HR.LeaveManagement.BlazorWASM.Contracts;
using HR.LeaveManagement.BlazorWASM.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorWASM.Pages.LeaveTypes
{
    public partial class Index
    {
        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Inject]
        public required ILeaveTypeService LeaveTypeService { get; set; }

        [Inject]
        public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public IReadOnlyList<LeaveTypeViewModel> LeaveTypes { get; private set; } = [];

        public string Message { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            LeaveTypes = await LeaveTypeService.GetLeaveTypes();
        }

        protected void CreateLeaveType()
        {
            NavigationManager.NavigateTo("/leavetypes/create");
        }

        protected void AllocationLeaveType(int id)
        {
            
        }

        protected void EditLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
        }

        protected void DetailsLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/details/{id}");
        }

        protected async Task DeleteLeaveTypeAsync(int id)
        {
            var response = await LeaveTypeService.DeleteLeaveType(id);
            if (response.Success)
            {
                LeaveTypes = await LeaveTypeService.GetLeaveTypes();
                StateHasChanged();
            }
            else
            {
                Message = response.Message;
            }
        }
    }
}