Partial Public Class RouteHis
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetData()
            GetMainCategories()
        End If
    End Sub

    Protected Sub GetData()
        Dim routeID As Integer = If(Not IsNumeric(Request("rt")), 45, Integer.Parse(Request("rt")))
        Dim nr As nftaRoute = New nftaRoute
        Dim TheRoute() As String = nr.GetRouteById(routeID)

        'ROUTE ID (is not the same as Route Number)
        Dim intRouteID As Integer = TheRoute(0)

        'ROUTE Number --------------------------
        Dim strRouteNumber As String = TheRoute(3)

        'ROUTE Name --------------------------
        Dim strRouteName As String = TheRoute(2)

        'ROUTE Display of FRIENDLY Name
        If strRouteNumber = "45" Then
            litRouteDisplayName.Text = strRouteName
        ElseIf strRouteNumber = "56" Then
            litRouteDisplayName.Text = "<span style='font-weight:100;'><em>Route #55T</em></span> " & strRouteName
        Else
            litRouteDisplayName.Text = "<span style='font-weight:100;'><em>Route #" & strRouteNumber & "</em></span> " & strRouteName
        End If

        'STATUS ALERT BOX --------------------------
        nftaRoute.ActiveAlerts = nftaRoute.GetActiveAlerts(intRouteID, 0)  '1=3 and 0=all

        If Not IsNothing(nftaRoute.ActiveAlerts) Then
            Dim alerts As StringBuilder = New StringBuilder
            For Each dr In nftaRoute.ActiveAlerts.Rows
                If dr.Item("RouteId") = intRouteID Then alerts.Append("<div class='theAlertDetails color" & dr.Item("StatusColor") & "'><div class='AlertTitle'>" & litRouteDisplayName.Text & "</div><div class='AlertStamp'><em>posted: </em>" & dr.item("DateAdded") & "</div><br clear='all' />" & dr.item("MessageEmail") & "</div>")
            Next
            litAlert.Text &= If(Not String.IsNullOrEmpty(alerts.ToString), _
                "<div id='statusAlertHistory'><h5>Staus Alert History</h5><p class='txtCenter'>May include Rider Alerts, Special Bulletins and other route specific information.</p>" & _
                alerts.ToString & _
                "</div>", Nothing)
        End If
    End Sub

    Protected Sub GetMainCategories()
        Dim results = nftaRoute.GetMainRouteCategories
        Dim tbl As DataTable = New DataTable
        If Not IsNothing(results) Then
            With tbl
                .Columns.Add("Category_Id", GetType(Integer))
                .Columns.Add("CategoryName", GetType(String))
            End With
            Dim rw As DataRow
            For Each result In results
                rw = tbl.NewRow
                rw.Item("Category_Id") = result.Category_Id
                rw.Item("CategoryName") = result.CategoryName
                tbl.Rows.Add(rw)
            Next
        End If
        rptCategories.DataSource = tbl
        rptCategories.DataBind()
    End Sub

    Private Sub rptCategories_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptCategories.ItemDataBound
        Dim rpt As Repeater = e.Item.FindControl("rptRoutes")
        Dim results = nftaRoute.GetRoutes(e.Item.DataItem(0))
        Dim tbl As DataTable = New DataTable
        If Not IsNothing(results) Then
            With tbl
                .Columns.Add("RouteId", GetType(Integer))
                .Columns.Add("RouteName", GetType(String))
                .Columns.Add("RouteNumber", GetType(String))
                .Columns.Add("RouteDisplayNumber", GetType(String))
                .Columns.Add("Rev", GetType(String))
            End With
            Dim rw As DataRow
            For Each result In results
                rw = tbl.NewRow
                rw.Item("RouteId") = result.RouteId
                rw.Item("RouteName") = result.RouteName
                rw.Item("RouteNumber") = result.RouteNumber
                rw.Item("RouteDisplayNumber") = If(Not result.RouteNumber = "45", "<em>#" & result.RouteNumber & "</em>", Nothing)
                rw.Item("Rev") = If(Not String.IsNullOrEmpty(result.Revised), _
                    result.Revised, Nothing)
                tbl.Rows.Add(rw)
            Next
        End If
        rpt.DataSource = tbl
        rpt.DataBind()
    End Sub

End Class