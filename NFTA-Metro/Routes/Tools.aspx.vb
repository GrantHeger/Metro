Public Partial Class Tools
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadRiderAlerts()
    End Sub

    Protected Sub LoadRiderAlerts()

        Dim tbl As DataTable = nftaRoute.GetDistinctActiveAlerts()
        If Not IsNothing(tbl) Then
            For Each rw In tbl.Rows
                'litAlertLinks.Text &= "<a href='http://metro.nfta.com/Routes/Route.aspx?rt=" & rw.Item("RouteNumber").ToString & "' Title='Click here to view Route details'>" & If(rw.Item("RouteNumber").ToString = "45", "Rail", rw.Item("RouteNumber").ToString & " - " & rw.Item("RouteName")) & " (" & rw.Item("Number").ToString & ")" & "</a> &nbsp; "
                litAlertLinks.Text &= "<div class='alert'><a href='http://metro.nfta.com/Routes/Route.aspx?rt=" & rw.Item("RouteNumber").ToString & "' Title='Click here to view Route details'>" & If(rw.Item("RouteNumber").ToString = "45", "Rail", rw.Item("RouteNumber").ToString & "&nbsp;-&nbsp;" & rw.Item("RouteName")) & "</a></div>"
            Next
        End If
        phNoAlerts.Visible = If(tbl.Rows.Count > 0, False, True)
    End Sub

End Class