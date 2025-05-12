Imports System.Net.Http
Imports Microsoft.Extensions.Configuration

Public Class Menu
    Private Const DisplayAllOption As String = "1"
    Private Const DisplayByIdOption As String = "2"
    Private Const ExitOption As String = "3"

    Private ReadOnly _client As HttpClient
    Private ReadOnly _config As IConfiguration

    Public Sub New(client As HttpClient, config As IConfiguration)
        _client = client
        _config = config
    End Sub

    Public Async Function StartAsync() As Task
        Dim isRunning As Boolean = True

        While isRunning
            Console.Clear()
            DisplayMenu()

            Dim userChoice As String = Console.ReadLine()

            Select Case userChoice
                Case DisplayAllOption
                    Await DisplayLeaveTypesAsync()
                Case DisplayByIdOption
                    Await DisplayLeaveTypeByIdAsync()
                Case ExitOption
                    isRunning = False
                Case Else
                    Console.WriteLine("Invalid option, please select a valid option.")
                    Await Task.Delay(1000)
            End Select
        End While

        Console.WriteLine("Program has ended.")
    End Function

    Private Shared Sub DisplayMenu()
        Console.WriteLine("Select an option:")
        Console.WriteLine($"{DisplayAllOption}. Display Leave Types")
        Console.WriteLine($"{DisplayByIdOption}. Display Leave Type by ID")
        Console.WriteLine($"{ExitOption}. Exit")
        Console.Write("Enter your choice: ")
    End Sub

    Private Async Function DisplayLeaveTypesAsync() As Task
        Dim leaveTypeService As New LeaveTypeService(_client, _config)
        Await leaveTypeService.ILeaveTypeService_DisplayLeaveTypesAsync()
    End Function

    Private Async Function DisplayLeaveTypeByIdAsync() As Task
        Console.Write("Enter Leave Type ID: ")
        Dim input = Console.ReadLine()
        Dim id As Integer

        If Not Integer.TryParse(input, id) Then
            Console.WriteLine("Invalid ID format.")
            Console.ReadKey()
            Return
        End If

        Dim service As New LeaveTypeService(_client, _config)
        Await service.ILeaveTypeService_DisplayLeaveTypeByIdAsync(id)
    End Function
End Class
