Partial Public Class _dp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        phForm.Visible = "true"
        phSent.Visible = "false"
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim strName As String = tbName.Text
        Dim strCompany As String = tbCompany.Text
        Dim strPhone As String = tbPhone.Text
        Dim strEmail As String = tbEmail.Text
        Dim strComments As String = tbComments.Text
        Dim mailTo As String = "pr@nfta.com"
        'Dim mailTo As String = "corey_hacker@nfta.com"
        Dim mailSubject As String = "Metro Website Dump the Pump"
        Dim mailBody As String = "<b>Metro Website Dump the Pump Testimonial</b><br />" & strName & "<br />" & strCompany & "<br />" & strPhone & "<br />" & strEmail & "<br /><br />" & strComments & "<br /><br />User Agent: " & Request.UserAgent & "<br />IP Address: " & Request.UserHostAddress


        'Dim myMail As New System.Net.Mail.MailMessage("pr@nfta.com", mailTo, mailSubject, mailBody)
        Dim myMail As New System.Net.Mail.MailMessage("pr@niagarafrontiertransportationauthority.com", mailTo, mailSubject, mailBody)
        myMail.Bcc.Add("corey_hacker@nfta.com")
        myMail.Bcc.Add("lisa_piecki@nfta.com")
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