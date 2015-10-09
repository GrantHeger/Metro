Imports System.IO

Partial Public Class _DefaultDb
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadEmergency()
        'LoadRiderAlerts()
        LoadMessageBoard()
        LoadRoutes()
    End Sub

    'Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    Select Case RadioButtonList1.SelectedItem.Text
    '        Case "Panel1"
    '            Panel1.Visible = True
    '            Panel2.Visible = False
    '        Case "Panel2"
    '            Panel1.Visible = False
    '            Panel2.Visible = True
    '    End Select
    'End Sub

    Sub btnToggle_OnClick(ByVal sender As Object, _
                          ByVal e As EventArgs)


        ' When the button is clicked,
        ' change the button text, and disable it.

        Dim clickedButton As Button = sender

        If clickedButton.Text = "Hide Route Names" Then
            clickedButton.Text = "Show Route Names"
            Panel1.Visible = True
            Panel2.Visible = False
        Else
            clickedButton.Text = "Hide Route Names"
            Panel1.Visible = False
            Panel2.Visible = True

        End If
        
    End Sub



    Protected Sub LoadRoutes()

        Dim routeTbl As DataTable = nftaRoute.GetCurrentRouteStatus(0)
        If Not IsNothing(routeTbl) Then
            Dim tbl As DataTable = New DataTable
            With tbl
                .Columns.Add("RouteId", GetType(Integer))
                .Columns.Add("RouteNumber", GetType(String))
                .Columns.Add("RouteName", GetType(String))
                .Columns.Add("StatusColor", GetType(Integer))
                .Columns.Add("RouteDisplayNumber", GetType(String))
                .Columns.Add("RouteNumberAndName", GetType(String))
                .Columns.Add("RouteStatusMessage", GetType(String))
                .Columns.Add("DateAdded", GetType(DateTime))
            End With
            Dim rw As DataRow
            For Each result In routeTbl.Rows
                rw = tbl.NewRow
                rw.Item("RouteId") = result.Item("RouteId")
                rw.Item("RouteNumber") = result.Item("RouteNumber")
                rw.Item("RouteName") = result.Item("RouteName")
                rw.Item("StatusColor") = result.Item("StatusColor")
                rw.Item("RouteDisplayNumber") = If(result.Item("RouteNumber").ToString = "45", "Rail", result.Item("RouteNumber"))
                rw.Item("RouteNumberAndName") = If(result.Item("RouteNumber").ToString = "45", "Rail", result.Item("RouteNumber") & "-" & result.Item("RouteName"))
                rw.Item("RouteStatusMessage") = result.Item("StatusMessage")
                rw.Item("DateAdded") = result.Item("DateAdded")
                tbl.Rows.Add(rw)
            Next
            rptRoutes1.DataSource = tbl
            rptRoutes1.DataBind()
            rptRoutes2.DataSource = tbl
            rptRoutes2.DataBind()
        End If
    End Sub

    'Protected Sub LoadRiderAlerts()
    '    Dim tbl As DataTable = nftaRoute.GetDistinctActiveAlerts()
    '    If Not IsNothing(tbl) Then
    '        For Each rw In tbl.Rows
    '            'litAlertLinks.Text &= "<a href='http://metro.nfta.com/Routes/Route.aspx?rt=" & rw.Item("RouteNumber").ToString & "' Title='Click here to view Route details'>" & If(rw.Item("RouteNumber").ToString = "45", "Rail", rw.Item("RouteNumber").ToString & " - " & rw.Item("RouteName")) & " (" & rw.Item("Number").ToString & ")" & "</a> &nbsp; "
    '            litAlertLinks.Text &= "<div class='alert'><a href='http://metro.nfta.com/Routes/Route.aspx?rt=" & rw.Item("RouteNumber").ToString & "' Title='Click here to view Route details'>" & If(rw.Item("RouteNumber").ToString = "45", "Rail", rw.Item("RouteNumber").ToString & "&nbsp;-&nbsp;" & rw.Item("RouteName")) & "</a></div>"
    '        Next
    '    End If
    '    phNoAlerts.Visible = If(tbl.Rows.Count > 0, False, True)
    'End Sub

    Protected Sub LoadEmergency()
        Dim FilePath As String = (System.Configuration.ConfigurationManager.GetSection("appSettings")("EmergencyDataPath").ToString)
        Dim MyFileInfo As FileInfo = New FileInfo(FilePath)

        Dim current As XMLEmergencyHandler = New XMLEmergencyHandler()
        current.LoadReport()

        EmergencyText.Text = current.Message
        EmergencyStatus.Text = If(String.IsNullOrEmpty(EmergencyText.Text), "style='display:none;'", String.Empty)
    End Sub

    Protected Sub DetectUserAgent()
        Dim strDevice As String
        strDevice = (Request.QueryString("S"))
        If strDevice = "Full" Then
            'show Full site
        Else
            'check user agent 
            Dim strUserAgent As String
            strUserAgent = (Request.ServerVariables("HTTP_USER_AGENT"))
            'Response.Write(strUserAgent)
            If strUserAgent.Contains("iPhone") Or _
                strUserAgent.Contains("windows ce") Or _
                strUserAgent.Contains("blackberry") Or _
                strUserAgent.Contains("opera mini") Or _
                strUserAgent.Contains("mobile") Or _
                strUserAgent.Contains("palm") Or _
                strUserAgent.Contains("portable") Then
                Response.Redirect("http://m.nfta.com/#metro")
            End If
        End If
    End Sub

    Protected Sub LoadMessageBoard()
        Dim FilePath As String = (System.Configuration.ConfigurationManager.GetSection("appSettings")("MessageBoardDataPath").ToString)
        Dim MyFileInfo As FileInfo = New FileInfo(FilePath)
        Dim current As XMLMessageBoardHandler = New XMLMessageBoardHandler()
        current.LoadReport()
        If current.Message = "" Then
            MessageBoardText.Text = ""
        Else
            MessageBoardText.Text = "<div id='MessageBoard'><p class='TTHeading'><img src='/img/h1-messageboard.png' width='240' height='50' alt='Message Board' /></p>" & current.Message & "</div>"
        End If
    End Sub

    'Sub btnRouteDetails_OnClick_Orig(ByVal Src As Object, ByVal E As EventArgs)
    '    Dim btn As Button = rptRoutes.FindControl("Button1")
    '    Response.Redirect("/Routes/Route.aspx?rt=" & btn.CommandArgument)
    'End Sub

    Sub btnRouteDetails_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = CType(sender, Button)
        Response.Redirect("/Routes/Route.aspx?rt=" & btn.CommandArgument)
    End Sub

    'Sub btnToggleDispaly_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
    'Dim btnToggleDispaly As Button
    'btnToggleDispaly.Text = "Hide Route Names"
    'If btnToggleDispaly.Text = "Show Route Names" Then
    'btnToggleDispaly.Text = "Hide Route Names"
    'phDisplayRtNum.Visible = False
    'phDisplayRtName.Visible = True
    'ElseIf btnToggleDispaly.Text = "Hide Route Names" Then
    'btnToggleDispaly.Text = "Show Route Names"
    'phDisplayRtNum.Visible = True
    'phDisplayRtName.Visible = False
    'End If
    'End Sub

    Public Shared Function DetectUserAgent(ByVal userAgent As String) As Boolean
        userAgent = userAgent.ToLower()
    End Function

   
End Class

