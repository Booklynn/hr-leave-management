﻿namespace HR.LeaveManagement.BlazorWASM.Services.Base;

public class Response<T>
{
    public string Message { get; set; } = string.Empty;
    public string? ValidatioErrors { get; set; }
    public bool Success { get; set; }
    public T? Data { get; set; }
}
