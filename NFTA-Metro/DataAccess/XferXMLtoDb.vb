Imports System.Data
Imports System.Data.SqlClient

Public Class XferXMLtoDb

    Public Shared Sub TransferRoutes()
        Dim ds As New DataSet
        ds.ReadXml(System.Configuration.ConfigurationManager.GetSection("appSettings")("RoutesDataPath").ToString)
        If ds.Tables(0).Rows.Count > 0 Then
            For Each rw As DataRow In ds.Tables(0).Rows
                Dim command As SqlCommand = New SqlCommand("spLocal_Route_Add")
                Dim routename As String = Replace(Replace(rw.Item("name"), "<![CDATA[", ""), "]]>", "")
                Dim copy As String = Replace(Replace(rw.Item("copy"), "<![CDATA[", ""), "]]>", "")
                Dim note As String = Replace(Replace(rw.Item("Note"), "<![CDATA[", ""), "]]>", "")
                command.Parameters.AddWithValue("@catId", If(rw.Item("category").ToString.ToLower = "metro bus", 1, 2))
                command.Parameters.AddWithValue("@name", If(Not String.IsNullOrEmpty(routename), routename.Trim, Nothing))
                command.Parameters.AddWithValue("@number", If(Not String.IsNullOrEmpty(rw.Item("routeNum")), rw.Item("routeNum").ToString.Trim, Nothing))
                command.Parameters.AddWithValue("@status", If(rw.Item("status").ToString.ToLower = "active", 1, 0))
                command.Parameters.AddWithValue("@copy", If(Not String.IsNullOrEmpty(copy), copy.Trim, Nothing))
                command.Parameters.AddWithValue("@spanish", If(rw.Item("spanish").ToString.ToLower = "true", 1, 0))
                command.Parameters.AddWithValue("@english", If(rw.Item("english").ToString.ToLower = "true", 1, 0))
                command.Parameters.AddWithValue("@pages", If(IsNumeric(rw.Item("pages")), rw.Item("pages"), Nothing))
                command.Parameters.AddWithValue("@map", If(rw.Item("map").ToString.ToLower = "true", 1, 0))
                command.Parameters.AddWithValue("@note", If(Not String.IsNullOrEmpty(note), note.Trim, Nothing))
                StoredProcedureReader.ReadOutput(command)
            Next
        End If
    End Sub

End Class
