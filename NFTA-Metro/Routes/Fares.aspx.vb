Partial Public Class _Fares

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'DisplayMonth()
    End Sub

    'Protected Sub DisplayMonth()
    'litCurrentMonth.Text = Today.ToString("MMMM")
    'litNextMonth.Text = DateTime.Now.AddMonths(+1).ToString("MMMM")
    'End Sub

    Protected PP_URL As String = "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick"
    'Protected PP_URL As String = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_s-xclick"  'Sandbox

    '-----MONTHLY PASS ---------
    Protected MP_ID As String = "YU7Y63RAZVELL"
    Protected _MPType As String = ""
    Protected _MPMonth As String = ""




    '-----PAL PASS ---------
    Protected PAL_ID As String = "JJZGE8QFKUPQ6"
    Protected _PALTrips As String = ""

    '-----SUMMER YOUTH PASS ---------
    Protected SYP_ID As String = "DZ32WXNLCJBAW"

    Protected Sub btnMP_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        _MPType = Request("os0-MP")
        _MPMonth = Request("os1-MP")
        Response.Redirect(PP_URL & "&hosted_button_id=" & MP_ID & "&on0=PassType&os0=" & _MPType & "&on1=Month&os1=" & _MPMonth)
    End Sub

    Protected Sub btnPAL_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        _PALTrips = Request("os0-PAL")
        Response.Redirect(PP_URL & "&hosted_button_id=" & PAL_ID & "&on0=Trips&os0=" & _PALTrips)
    End Sub

    Protected Sub btnSYP_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect(PP_URL & "&hosted_button_id=" & SYP_ID)
    End Sub

End Class