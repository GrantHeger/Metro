Imports System.IO

Public Class LogErrors

    Public Shared Sub LogError(ByVal err As String, ByVal TheMethod As String)
       LogItem(err, TheMethod, "errors.txt")
    End Sub

    Private Shared Sub LogItem(ByVal details As String, ByVal theMethod As String, ByVal filename As String)
        Dim objStreamWriter As StreamWriter = File.AppendText(System.Configuration.ConfigurationManager.GetSection("appSettings")("BaseDirectory").ToString & "logs\" & filename)
        objStreamWriter.WriteLine(HttpUtility.HtmlDecode(DateTime.Now.ToString & "|" & HttpContext.Current.Request.ServerVariables("URL") & "|" & theMethod & "|" & _
            HttpContext.Current.Request.ServerVariables("REMOTE_ADDR") & "|" & HttpContext.Current.Request.ServerVariables("HTTP_REFERER") & "|" & details & vbCrLf))
        objStreamWriter.Close()
    End Sub

End Class
