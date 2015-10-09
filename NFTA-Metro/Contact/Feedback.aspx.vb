Partial Public Class _feed
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        phForm.Visible = "true"
        phSent.Visible = "false"
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim strName As String = tbName.Text
        Dim strEmail As String = tbEmail.Text
        Dim strComments As String = tbComments.Text
        Dim mailTo As String = "pr@nfta.com"
        Dim mailSubject As String = "Metro System Map Feedback"
        Dim mailBody As String = "<b>Metro System Map Feedback Form</b><br />" & strName & "<br />" & strEmail & "<br /><br />" & strComments & "<br /><br />" & Request.UserAgent

        Dim myMail As New System.Net.Mail.MailMessage("pr@niagarafrontiertransportationauthority.com", mailTo, mailSubject, mailBody)
        myMail.Bcc.Add("corey_hacker@nfta.com")
        'myMail.Bcc.Add("pr@nfta.com")
        myMail.IsBodyHtml = True

        Try
            Dim c As New System.Net.Mail.SmtpClient()
            c.Send(myMail)

            phForm.Visible = "false"
            phSent.Visible = "true"
        Catch myEx As System.Net.Mail.SmtpException
            System.Diagnostics.Debug.WriteLine("Error sending mail: " & myEx.Message)
            Throw myEx
        Finally
            myMail = Nothing
        End Try

    End Sub



End Class