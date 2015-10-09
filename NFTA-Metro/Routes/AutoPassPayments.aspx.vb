Partial Public Class _AutoPass
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub


    Protected PP_URL As String = "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick"
    'Protected PP_URL As String = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_s-xclick"  'Sandbox

    '-----MONTHLY PASS ---------
    Protected Auto_ID As String = "ZNPMMNMTK8S8Y"
    Protected _AutoType As String = ""


    Protected Sub btnAuto_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        _AutoType = Request("os0-Auto")
        Response.Redirect(PP_URL & "&hosted_button_id=" & Auto_ID & "&on0=Pass&os0=" & _AutoType)
    End Sub

End Class
