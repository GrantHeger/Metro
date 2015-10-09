Partial Public Class MetroNow
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then LoadAlerts()
    End Sub

    Protected Sub LoadAlerts()
        rptAlerts.DataSource = GetData()
        rptAlerts.DataBind()
    End Sub

    Protected Function GetData() As DataTable
        Dim results As DataTable = nftaRoute.GetActiveAlerts(0, 0)
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
                    .Columns.Add("Rev", GetType(String))
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
                    rw.Item("Rev") = If(Not String.IsNullOrEmpty(results(i).Item("Revised")), results(i).Item("Revised"), Nothing)
                    rw.Item("StatusColor") = results(i).Item("StatusColor")
                    rw.Item("DateAdded") = results(i).Item("DateAdded")
                    rw.Item("MessageEmail") = results(i).Item("MessageEmail")
                    tbl.Rows.Add(rw)
                Next
            End If
        End If
        Return tbl
    End Function

    Private Sub ddlNumToShow_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlNumToShow.SelectedIndexChanged
        rptAlerts.DataSource = GetData()
        rptAlerts.DataBind()
    End Sub

End Class