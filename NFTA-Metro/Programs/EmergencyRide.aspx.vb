Imports System.Net.Mail


Partial Public Class _EmergencyRide
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        phForm.Visible = "false"
        phSent.Visible = "false"
        phTemp.Visible = "True"
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnER.Click


        Dim STRname As String = name.Text
        Dim STRaddress As String = address.Text
        Dim STRcity As String = city.Text
        Dim STRzip As String = zip.Text
        Dim STRhomephone As String = homephone.Text
        Dim STRworkphone As String = workphone.Text
        Dim STRemployer As String = employer.Text
        Dim STRemployeraddress As String = employeraddress.Text
        Dim STRflashnumber As String = flashnumber.Text
        Dim STRwhenpurchased As String = whenpurchased.Text
        Dim STRwherepurchased As String = wherepurchased.Text
        Dim STRroutesused As String = routesused.Text
        'Dim mailTo As String = "coreyhacker@gmail.com"
        Dim mailTo As String = "info@nfta.com"
        Dim mailSubject As String = "Emergency Ride"
        Dim mailBody As String = "Name: " & STRname & "<br /><br />" & STRaddress & "<br />" & STRcity & "<br />" & STRzip & "<br /><br />Phone: " & STRhomephone & "<br />Work: " & STRworkphone & "<br /><br />Employeer: " & STRemployer & "<br />" & STRemployeraddress & "<br /><br />Flash Number: " & STRflashnumber & "<br /><br />Purchased:<br />" & STRwhenpurchased & "<br />" & STRwherepurchased & "<br /><br />Routes Used: " & STRroutesused



        Dim myMail As New System.Net.Mail.MailMessage("admin@niagarafrontiertransportationauthority.com", mailTo, mailSubject, mailBody)
        myMail.Bcc.Add("corey_hacker@nfta.com")
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