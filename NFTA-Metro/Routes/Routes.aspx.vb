Partial Public Class Routes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("/routes/schedules.aspx")

        nftaRoute.ActiveAlerts = nftaRoute.GetActiveAlerts
        GetMainCategories()
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
                .Columns.Add("ToolTip", GetType(String))
                .Columns.Add("Alert", GetType(String))
                .Columns.Add("Rev", GetType(String))
                .Columns.Add("Icons", GetType(String))
                .Columns.Add("Schedule", GetType(String))
            End With
            Dim rw As DataRow
            For Each result In results
                rw = tbl.NewRow
                rw.Item("RouteId") = result.RouteId
                rw.Item("RouteName") = result.RouteName
                'rw.Item("RouteNumber") = If(result.RouteNumber = "45", Nothing, "<br /><em><a name ='" & result.RouteNumber & "'</a> Route # " & result.RouteNumber)
                rw.Item("RouteNumber") = If(result.RouteNumber = "45", "<a name ='" & result.RouteNumber & "'></a>", "<em>Route #<a name ='" & result.RouteNumber & "'>" & result.RouteNumber & "</a></em>")
                rw.Item("ToolTip") = ""
                'rw.Item("ToolTip") = If(String.IsNullOrEmpty(result.Copy), Nothing, _
                '                                "<a class='showTip'><img src='../img/tip.gif' width='20' height='20' alt='Route Information' /></a>" & _
                '                                "<div class='tooltipBlue'>" & _
                '                                    "<div class='toolTipInner'>" & _
                '                                        "<h2>Route # " & result.RouteNumber & " " & result.RouteName & "</h2>" & _
                '                                            result.Copy & _
                '                                    "</div>" & _
                '                                "</div>")
                If Not IsNothing(nftaRoute.ActiveAlerts) Then
                    
                End If

                'If Not IsNothing(nftaRoute.ActiveAlerts) Then
                '    Dim alerts As StringBuilder = New StringBuilder
                '    For Each dr In nftaRoute.ActiveAlerts.Rows
                '        If dr.Item("RouteNumber") = result.RouteNumber Then alerts.Append("<h2>" & dr.item("DateAdded") & "</h2>" & dr.item("MessageEmail"))
                '    Next
                '    rw.Item("Alert") = If(Not String.IsNullOrEmpty(alerts.ToString), _
                '        "<a class='showTip'><img src='../img/alert.gif' width='25' height='25' alt='Route Alerts' /></a>" & _
                '            "<div class='tooltipRed'>" & _
                '                "<div class='toolTipInner'>" & alerts.ToString & "</div></div>", Nothing)
                '    End


                'rw.Item("Rev") = If(String.IsNullOrEmpty(result.Revised), Nothing, _
                '                                "<a class='showTip'><img src='../img/iconRevised.gif' width='100' height='30' alt='Schedule has been Revised' /></a>" & _
                '                                "<div class='tooltipRed'>" & _
                '                                    "<div class='toolTipInner'>" & _
                '                                        "<h2>Schedule has been Revised</h2>" & _
                '                                            result.Revised & _
                '                                    "</div>" & _
                '                                "</div>")

                If result.Map = False And result.English = False Then
                    rw.Item("Icons") = If(Not String.IsNullOrEmpty(result.Note), result.Note, System.Configuration.ConfigurationManager.GetSection("appSettings")("ShowNextDefaultMessage"))
                Else

                    rw.Item("Icons") = ""
                    'rw.Item("Icons") = If(result.Map = False, Nothing, _
                    '"<img class='hand' src='../img/icon-map.gif' width='80' height='50' alt='' rel='#map_" & result.RouteNumber & "' />" & _
                    '"<div class='map_overlay' id='map_" & result.RouteNumber & "'>" & _
                    '    "<div class='details'>" & _
                    '        "<img src='/Routes/maps/" & result.RouteNumber & ".gif' alt='" & result.RouteNumber & " " & result.RouteName & " Map'>" & _
                    '    "</div>" & _
                    '"</div>")


                    If result.English Then
                        rw.Item("Icons") &= "&nbsp;<a href='/Routes/ttpdf/" & result.RouteNumber & ".pdf' target='_" & result.RouteNumber & "pdf'><img src='../img/icon-schedule.gif'  width='80' height='50' alt='View (PDF) Schedule'></a>"
                    End If

                    If result.TimeTable Then
                        rw.Item("Icons") &= "&nbsp;<a href='/Routes/timeTables/" & result.RouteNumber & ".pdf' target='_" & result.RouteNumber & "pdf'><img src='../img/icon-timetable.gif'  width='80' height='50' alt='View (PDF) Time Table'></a>"
                    End If

                End If
                rw.Item("Schedule") = result.CategoryName
                tbl.Rows.Add(rw)
            Next
        End If
        rpt.DataSource = tbl
        rpt.DataBind()
    End Sub

End Class