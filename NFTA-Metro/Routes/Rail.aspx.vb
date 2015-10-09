Partial Public Class _Rail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("http://metro.nfta.com/Routes/Route.aspx?rt=45")
    End Sub

End Class