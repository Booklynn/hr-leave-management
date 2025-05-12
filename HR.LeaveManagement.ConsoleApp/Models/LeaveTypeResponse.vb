Imports System.Text.Json.Serialization

Public Class LeaveTypeResponse
    <JsonPropertyName("id")>
    Public ReadOnly Property Id As Integer
    <JsonPropertyName("name")>
    Public ReadOnly Property Name As String
    <JsonPropertyName("defaultDays")>
    Public ReadOnly Property DefaultDays As Integer

    Public Sub New(id As Integer, name As String, defaultDays As Integer)
        Me.Id = id
        Me.Name = name
        Me.DefaultDays = defaultDays
    End Sub
End Class
