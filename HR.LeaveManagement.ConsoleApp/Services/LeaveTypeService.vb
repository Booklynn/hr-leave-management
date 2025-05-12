Imports System.Net
Imports System.Net.Http
Imports System.Text.Json
Imports Microsoft.Extensions.Configuration

Public Class LeaveTypeService
    Implements ILeaveTypeService

    Private ReadOnly _client As HttpClient
    Private ReadOnly _apiBaseUrl As String

    Public Sub New(client As HttpClient, config As IConfiguration)
        _client = client
        _apiBaseUrl = config.GetValue(Of String)("ApiBaseUrl")
    End Sub

    Public Async Function ILeaveTypeService_DisplayLeaveTypesAsync() As Task Implements ILeaveTypeService.DisplayLeaveTypesAsync
        Try
            Using stream = Await _client.GetStreamAsync(_apiBaseUrl)
                Dim leaveTypes = Await JsonSerializer.DeserializeAsync(Of List(Of LeaveTypeResponse))(stream)
                If leaveTypes IsNot Nothing AndAlso leaveTypes.Count > 0 Then
                    Console.WriteLine(Environment.NewLine & "Leave Type" & Environment.NewLine)
                    For Each leaveType In leaveTypes
                        Console.WriteLine($"Id: {leaveType.Id}")
                        Console.WriteLine($"Name: {leaveType.Name}")
                        Console.WriteLine($"Default Days: {leaveType.DefaultDays}")
                        Console.WriteLine()
                    Next
                Else
                    Console.WriteLine("No leave types found.")
                End If
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try

        Console.WriteLine("Press any key to return to the menu...")
        Console.ReadKey()
    End Function

    Public Async Function ILeaveTypeService_DisplayLeaveTypeByIdAsync(id As Integer) As Task Implements ILeaveTypeService.DisplayLeaveTypeByIdAsync
        Try
            Dim url = $"{_apiBaseUrl}/{id}"
            Dim response = Await _client.GetAsync(url)

            If response.StatusCode = HttpStatusCode.NotFound Then
                Console.WriteLine($"Leave type with ID {id} was not found.")
            ElseIf response.IsSuccessStatusCode Then
                Dim stream = Await response.Content.ReadAsStreamAsync()
                Dim leaveType = Await JsonSerializer.DeserializeAsync(Of LeaveTypeDetailsResponse)(stream)
                Console.WriteLine(Environment.NewLine & "Leave Type Details" & Environment.NewLine)
                Console.WriteLine($"Id: {leaveType.Id}")
                Console.WriteLine($"Name: {leaveType.Name}")
                Console.WriteLine($"Default Days: {leaveType.DefaultDays}")
                Console.WriteLine($"Date Created: {leaveType.DateCreated}")
                Console.WriteLine($"Date Modified: {leaveType.DateModified}")
                Console.WriteLine()
            Else
                Console.WriteLine($"Failed to fetch leave type. Status: {response.StatusCode}")
            End If
        Catch ex As Exception
            Console.WriteLine($"Error fetching leave type by ID: {ex.Message}")
        End Try

        Console.WriteLine("Press any key to return to the menu...")
        Console.ReadKey()
    End Function
End Class
