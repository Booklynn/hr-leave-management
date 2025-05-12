Imports System.IO
Imports Microsoft.Extensions.Configuration
Imports System.Net.Http

Module Program
    Sub Main()
        Dim config As IConfiguration = BuildConfiguration()
        RunAsync(config).GetAwaiter().GetResult()
    End Sub

    Private Function BuildConfiguration() As IConfiguration
        Return New ConfigurationBuilder() _
            .SetBasePath(Directory.GetCurrentDirectory()) _
            .AddJsonFile("appsettings.json", optional:=True, reloadOnChange:=True) _
            .Build()
    End Function

    Private Async Function RunAsync(config As IConfiguration) As Task
        Using client As New HttpClient()
            Dim menu As New Menu(client, config)
            Await menu.StartAsync()
        End Using
    End Function
End Module
