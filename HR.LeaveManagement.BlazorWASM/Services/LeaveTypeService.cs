﻿using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorWASM.Contracts;
using HR.LeaveManagement.BlazorWASM.Models.LeaveTypes;
using HR.LeaveManagement.BlazorWASM.Services.Base;

namespace HR.LeaveManagement.BlazorWASM.Services;

public class LeaveTypeService(
    IClient client, 
    ILocalStorageService localStorageService) 
    : BaseHttpService(client, localStorageService), ILeaveTypeService
{
    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeViewModel leaveType)
    {
        try
        {
            await AddBearerToken();
            var request = new CreateLeaveTypeCommand
            {
                Name = leaveType.Name,
                DefaultDays = leaveType.DefaultDays,
            };

            await _client.LeaveTypesPOSTAsync(request);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertAPIExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> DeleteLeaveType(int id)
    {
        try
        {
            await AddBearerToken();
            await _client.LeaveTypesDELETEAsync(id);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch(ApiException ex)
        {
            return ConvertAPIExceptions<Guid>(ex);
        }
    }

    public async Task<LeaveTypeViewModel> GetLeaveTypeDetails(int id)
    {
        await AddBearerToken();
        var leaveType = await _client.LeaveTypesGETAsync(id);
        return new LeaveTypeViewModel
        {
            Id = leaveType.Id,
            Name = leaveType.Name,
            DefaultDays = leaveType.DefaultDays
        };
    }

    public async Task<IReadOnlyList<LeaveTypeViewModel>> GetLeaveTypes()
    {
        await AddBearerToken();
        var leaveTypes = await _client.LeaveTypesAllAsync();

        return leaveTypes?.Select(source => new LeaveTypeViewModel
        {
            Id = source.Id,
            Name = source.Name,
            DefaultDays = source.DefaultDays
        }).ToArray() ?? [];
    }

    public async Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeViewModel leaveType)
    {
        try
        {
            await AddBearerToken();
            var request = new UpdateLeaveTypeCommand
            {
                Name = leaveType.Name,
                DefaultDays = leaveType.DefaultDays
            };
            await _client.LeaveTypesPUTAsync(id, request);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertAPIExceptions<Guid>(ex);
        }
    }
}
