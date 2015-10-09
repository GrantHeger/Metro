Imports System.IO

Partial Public Class _NowBoard
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadEmergency()
        If Not IsPostBack Then LoadAlerts()
    End Sub

    Protected Sub LoadEmergency()
        Dim FilePath As String = (System.Configuration.ConfigurationManager.GetSection("appSettings")("EmergencyDataPath").ToString)
        Dim MyFileInfo As FileInfo = New FileInfo(FilePath)

        Dim current As XMLEmergencyHandler = New XMLEmergencyHandler()
        current.LoadReport()

        EmergencyText.Text = current.Message
        EmergencyStatus.Text = If(String.IsNullOrEmpty(EmergencyText.Text), "style='display:none;'", String.Empty)
    End Sub

    Sub btnRouteDetails_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = CType(sender, Button)
        Response.Redirect("/Routes/Route.aspx?rt=" & btn.CommandArgument)
    End Sub

    Protected Sub LoadAlerts()
        rptAlerts.DataSource = GetData()
        rptAlerts.DataBind()
        AllSystemsGo.Visible = If(rptAlerts.Items.Count > 0, False, True)
    End Sub

    Protected Function GetData() As DataTable
        'Dim results As DataTable = nftaRoute.GetActiveAlerts(0, 0)
        Dim results As DataTable = nftaRoute.MetroNowAlerts(0, 0)
        Dim tbl As DataTable = New DataTable
        Dim Num2Show As Integer = Request.QueryString("show")
        Dim numRows As Integer = If(Num2Show > 0, Num2Show, 100)
        'Dim numRows As Integer = Integer.Parse(ddlNumToShow.SelectedItem.ToString)
        If Not IsNothing(results) Then
            If results.Rows.Count > 0 Then
                With tbl
                    .Columns.Add("RouteId", GetType(Integer))
                    .Columns.Add("RouteName", GetType(String))
                    .Columns.Add("RouteNumber", GetType(String))
                    .Columns.Add("RouteDisplayNumber", GetType(String))
                    '.Columns.Add("Rev", GetType(String))
                    .Columns.Add("StatusColor", GetType(String))
                    .Columns.Add("DateAdded", GetType(DateTime))
                    .Columns.Add("MessageEmail", GetType(String))
                End With
                Dim rw As DataRow
                For i As Integer = 0 To If(numRows > results.Rows.Count, results.Rows.Count - 1, numRows - 1)
                    rw = tbl.NewRow
                    rw.Item("RouteId") = results(i).Item("RouteId")
                    rw.Item("RouteName") = results(i).Item("RouteName")
                    rw.Item("RouteNumber") = results(i).Item("RouteNumber")
                    rw.Item("RouteDisplayNumber") = If(Not results(i).Item("RouteNumber") = "45", "<em>#" & results(i).Item("RouteNumber") & " - </em>", Nothing)
                    'rw.Item("Rev") = If(Not String.IsNullOrEmpty(results(i).Item("Revised")), results(i).Item("Revised"), Nothing)
                    rw.Item("StatusColor") = results(i).Item("StatusColor")
                    rw.Item("DateAdded") = results(i).Item("DateAdded")
                    rw.Item("MessageEmail") = results(i).Item("StatusMessage")
                    tbl.Rows.Add(rw)
                Next
            End If
        End If
        Return tbl
    End Function

End Class