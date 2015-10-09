Imports Microsoft.VisualBasic
Imports System.Xml

Public Class XMLMessageBoardHandler

    Public EmergencyId As String
    Public Message As String

    Public Sub LoadReport()
        Dim ds As New DataSet
        ds.ReadXml(System.Configuration.ConfigurationManager.GetSection("appSettings")("MessageBoardDataPath").ToString)
        Dim record = (From p In ds.Tables(0) Where p.Item("Id") = 0).FirstOrDefault
        If Not String.IsNullOrEmpty(record.Item("MetroHomepage")) Then
            Message = "" & record.Item("MetroHomepage").ToString & ""

        Else
            Message = ""  'Display nothing when blank
        End If
    End Sub

End Class

