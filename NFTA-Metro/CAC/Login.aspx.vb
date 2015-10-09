Partial Public Class _CACLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Sub btnLogin_OnClick(ByVal Src As Object, ByVal E As EventArgs)
        If txtUsername.Text = "cac" And txtPassword.Text = "nftac@c" Then
            FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, True)
        Else
            lblInvalid.Text = "Sorry... try again..."
        End If
    End Sub
End Class