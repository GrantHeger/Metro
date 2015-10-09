Partial Public Class Route
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

        'EFFECTIVE Date --------------------------
        litEffective.Text = "<em>Effective Date</em> <b>" & TheRoute(12) & "</b>"

        'STATUS ALERT BOX --------------------------
        nftaRoute.ActiveAlerts = nftaRoute.GetActiveAlerts(intRouteID, 1)  '1=3 and 0=all

        If Not IsNothing(nftaRoute.ActiveAlerts) Then
            Dim alerts As StringBuilder = New StringBuilder
            For Each dr In nftaRoute.ActiveAlerts.Rows
                'TO DO ===================================================================== change class below - color0=green, color1=yellow, color2=red =============
                If dr.Item("RouteId") = intRouteID Then alerts.Append("<div class='theAlertDetails color" & dr.Item("StatusColor") & "'><div class='AlertTitle'>" & litRouteDisplayName.Text & "</div><div class='AlertStamp'><em>posted: </em>" & dr.item("DateAdded") & "</div><br clear='all' />" & dr.item("MessageEmail") & "</div>")
            Next
            litAlert.Text &= If(Not String.IsNullOrEmpty(alerts.ToString), _
                "<div id='statusAlert'><h5>Metro Now - System status alerts.</h5>" & _
                alerts.ToString & _
                "<div><a class='btn btn-primary btn-xs pull-right' href='/routes/MetroNowHistory.aspx?rt=" & routeID & "'>view history</a></div>" & _
                "<div class='clearAll keepInformed'><a class='InstantUpdates' href='http://alerts.nfta.com'><img src='/img/footer-iu.png' class='domroll /img/footer-iu-o.png' width='50' alt='Instant Updates'></a> Receive status alerts by text or email<br />or follow us on Twitter <a href='http://twitter.com/NFTAMetro' target='_new'><img src='/img/icon-twitter.png' class='domroll /img/icon-twitter-o.png' height='29' alt='Follow us on Twitter' /></a></div>" & _
                "</div>", Nothing)
        End If


        'COPY Text --------------------------
        If String.IsNullOrEmpty(TheRoute(5)) Then
            litCopyTxt.Text = ""
        Else
            litCopyTxt.Text = "<p>" & TheRoute(5) & "</p>"
        End If

        Dim strParsedRtNumber As String
        If Len(strRouteNumber) = 3 Then                 '3 digit routes
            strParsedRtNumber = strRouteNumber
        ElseIf Len(strRouteNumber) = 2 Then             '2 digit routes
            strParsedRtNumber = "0" & strRouteNumber
        Else                                            '1 digit routes
            strParsedRtNumber = "00" & strRouteNumber
        End If


        'PREVIEW  --------------------------
        If String.IsNullOrEmpty(TheRoute(6)) Then
            litPreview.Text = "<h5>Schedule Change</h5><p align='center'>A preview of the next schedule change is not available.<br /><img src='/img/preview-pdf-gray.png' width='200' alt='Preview Schedule'/></a></p>"
        Else
            litPreview.Text = "<h5>Schedule Change</h5><p>This Schedule will be changing on<b> " & TheRoute(6) & "</b>.  To preview the schedule click on the orange Preview Schedule button below.</p><p class='noPadding' align='center'><a href='preview/" & strRouteNumber & ".pdf' target='_rev" & strRouteNumber & "'><img src='/img/preview-pdf-o.png' class='domroll /img/preview-pdf.png' width='200' alt='Preview Schedule'/></a></p>"
        End If


        'CHANGES  --------------------------
        If String.IsNullOrEmpty(TheRoute(14)) Then
            litChanges.Text = ""
        Else
            litChanges.Text = "<blockquote style='font-size:.9em; border:0;'><b><em>Revision History</em></b><br />" & TheRoute(14) & "</blockquote>"
        End If


        'DOWNLOAD PDF --------------------------
        litDownload.Text = "<p class='noPadding' align='center'><a href='pdfs/" & strRouteNumber & ".pdf' target='_rt" & strRouteNumber & "'><img src='/img/download-schedule-o.gif' class='domroll /img/download-schedule.gif' width='200' alt='download PDF schedule'/></a></p>"


        'NOTE Text --------------------------
        If String.IsNullOrEmpty(TheRoute(10)) Then
            litNote.Text = ""
        Else
            litNote.Text = "<p>" & TheRoute(10) & "</p>"
        End If


        'PAGES (show timetable links)  NOT USED - SAVE FOR FUTURE--------------------------
        Dim btnCalWeek As String = "<img src='/img/tt-cal-week.png' width='160' height='150' alt='Weekday Timetables' /></a>"
        Dim btnCalSat As String = "<img src='/img/tt-cal-sat.png' width='160' height='150' alt='Saturday Timetables' /></a>"
        Dim btnCalSun As String = "<img src='/img/tt-cal-sun.png' width='160' height='150' alt='Sunday Timetables' /></a>"

        '  /Routes/tt/w/t1###_0map.htm
        '       /Routes = Routes Directory
        '       /tt = tt Directory (timetables
        '       /w = weekday   /s = saturday   /h sunday/holiday  Directories
        '       /t1  = base prefix to file name
        '       ### = route number (1 would be 001) appended to the t1 base prefix
        '       _0 = inbond  _1 = outbound  Direction appended to the route number
        '       map = show map  leave out to hide map  
        '       .htm = file extention for vertical and map timetable pages
        '       .pdf = file extension for pdf printable timetable
        '       .html = file extension for horizontal timetable page

        Dim btnWeekIn As String = "<a href='/Routes/tt/w/t1" & strParsedRtNumber & "_0map.htm' target='_new'><img src='/img/btn-week-in.png' class='domroll /img/btn-week-in-o.png' width='180' height='63' alt='Weekday Inbound Timetable' /></a>"
        Dim btnWeekOut As String = "<a href='/Routes/tt/w/t1" & strParsedRtNumber & "_1map.htm' target='_new'><img src='/img/btn-week-out.png' class='domroll /img/btn-week-out-o.png' width='180' height='63' alt='Weekday Outbound Timetable' /></a>"

        Dim btnSatIn As String = "<a href='/Routes/tt/s/t1" & strParsedRtNumber & "_0map.htm' target='_new'><img src='/img/btn-sat-in.png' class='domroll /img/btn-sat-in-o.png' width='180' height='63' alt='Saturday Inbound Timetable' /></a>"
        Dim btnSatOut As String = "<a href='/Routes/tt/s/t1" & strParsedRtNumber & "_1map.htm' target='_new'><img src='/img/btn-sat-out.png' class='domroll /img/btn-sat-out-o.png' width='180' height='63' alt='Saturday Outbound Timetable' /></a>"

        Dim btnSunIn As String = "<a href='/Routes/tt/h/t1" & strParsedRtNumber & "_0map.htm' target='_new'><img src='/img/btn-sun-in.png' class='domroll /img/btn-sun-in-o.png' width='180' height='63' alt='Sunday Inbound Timetable' /></a>"
        Dim btnSunOut As String = "<a href='/Routes/tt/h/t1" & strParsedRtNumber & "_1map.htm' target='_new'><img src='/img/btn-sun-out.png' class='domroll /img/btn-sun-out-o.png' width='180' height='63' alt='Sunday Outbound Timetable' /></a>"

        litPages.Text = TheRoute(8)
        Dim casePages As String = litPages.Text

        Select Case casePages
            Case "1" 'Sunday
                litPages.Text = "<p align='center' class='pBorder'><b>Timetables:</b> To view schedule timetables, click on the link below.<br />" & btnSunIn & " " & btnSunOut & "</p>"

            Case "2" 'Saturday
                litPages.Text = "<p align='center' class='pBorder'><b>Timetables:</b> To view schedule timetables, click the link below.<br />" & btnSatIn & " " & btnSatOut & "</p>"

            Case "3" 'Weekday
                litPages.Text = "<p class='pBorder'><b>Timetables:</b> To view schedule timetables, click the link below.<br />" & btnWeekIn & " " & btnWeekOut & "</p>"

            Case "4" 'Sunday, Weekday
                litPages.Text = "<p align='center' class='pBorder'><b>Timetables:</b> To view schedule timetables, click on one of the links below.<br />" & btnWeekIn & " " & btnWeekOut & "<br />" & btnSunIn & " " & btnSunOut & "</p>"

            Case "5" 'Saturday, Weekday
                litPages.Text = "<p align='center' class='pBorder'><b>Timetables:</b> To view schedule timetables, click on one of the links below.<br />" & btnWeekIn & " " & btnWeekOut & "<br />" & btnSatIn & " " & btnSatOut & "</p>"

            Case "6" 'Sunday, Saturday
                litPages.Text = "<p align='center' class='pBorder'><b>Timetables:</b> To view schedule timetables, click on one of the links below.<br />" & btnSatIn & " " & btnSatOut & "<br />" & btnSunIn & " " & btnSunOut & "</p>"

            Case "7" 'None
                litPages.Text = "<p align='center' class='pBorder'><b>Timetables:</b>There are no timetables found for this Route</p>"

            Case "8" 'All- Sunday, Saturday, Weekday
                litPages.Text = "<p align='center' class='pBorder'><b>Timetables:</b> To view schedule timetables, click on one of the links below.</p>" & _
                "<table  align='center' width='100%'><tr><td align='center'>" & btnCalSun & "</td><td align='center'>" & btnCalWeek & "</td><td align='center'>" & btnCalSat & "</td></tr>" & _
                "<tr><td align='center'>" & btnSunIn & "<br />" & btnSunOut & "</td><td align='center'>" & btnWeekIn & "<br />" & btnWeekOut & "</td><td align='center'>" & btnSatIn & "<br />" & btnSatOut & "</td></tr></table>"
            Case Else
                litPages.Text = "<p align='center' class='pBorder'><b>Timetables:</b>There are no timetables found for this Route</p>"
        End Select
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