Imports System.Text.Json.Serialization

Public Class LeaveTypeDetailsResponse
    <JsonPropertyName("id")>
    Public ReadOnly Property Id As Integer
    <JsonPropertyName("name")>
    Public ReadOnly Property Name As String
    <JsonPropertyName("defaultDays")>
    Public ReadOnly Property DefaultDays As Integer

    <JsonPropertyName("dateCreated")>
    Public ReadOnly Property DateCreated As DateTime
    <JsonPropertyName("dateModified")>
    Public ReadOnly Property DateModified As DateTime

    Public Sub New(id As Integer, name As String, defaultDays As Integer, dateCreated As DateTime, dateModified As DateTime)
        Me.Id = id
        Me.Name = name
        Me.DefaultDays = defaultDays
        Me.DateCreated = dateCreated
        Me.DateModified = dateModified
    End Sub
End Class
