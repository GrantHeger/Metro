Imports System.Net.Mail

Partial Public Class _PerkOrder
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            FillStateDropDown()
            ddlState.SelectedValue = "NY"
            'contact.Focus()
        End If
        Dim d As DateTime = Now.ToShortDateString
        tbDate.Text = d
        phForm.Visible = "true"
        phSent.Visible = "false"
    End Sub

    Protected Sub FillStateDropDown()
        Dim svc As New StatesAndProvincesService.StatesAndProvincesService
        Dim results As Array = svc.GetStatesAndProvinces
        If Not IsNothing(results) Then
            ddlState.DataSource = results
            ddlState.DataValueField = "stpAbbreviation"
            ddlState.DataTextField = "stpName"
            ddlState.DataBind()
            ddlState.Items.Insert(0, New ListItem("State/Province", "0"))
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrder.Click


        Dim strDate As String = tbDate.Text
        Dim strCompany As String = tbCompany.Text
        Dim strAddress As String = tbAddress.Text
        Dim strCity As String = tbCity.Text
        Dim strState As String = ddlState.SelectedValue
        Dim strZip As String = tbZip.Text
        Dim strContact As String = tbContact.Text
        Dim strContactEmail As String = tbContactEmail.Text
        Dim strContactPhone As String = tbContactPhone.Text
        Dim strFullFare As String = tbFull.Text
        Dim strReducedFare As String = tbReduced.Text
        Dim strPal10 As String = tbPal10.Text
        Dim strPal20 As String = tbPal20.Text
        Dim strMonth As String = tbMonth.Text
        Dim mailTo As String = "perk@nfta.com"
        Dim mailSubject As String = "Metro Perk"
        Dim mailBody As String = "<img src='http://metro.nfta.com/img/logos/perk.gif' width='220' height='60' alt='Metro Perk' /><br /><b>Metro Perk Online Form</b><br /><em>" & strDate & "</em><br /><br /><em>Company:</em><br />" & strCompany & "<br />" & strAddress & "<br />" & strCity & ", " & strState & " " & strZip & "<br /><br /><em>Contact:</em><br />" & strContact & "<br />" & strContactEmail & "<br />" & strContactPhone & "<br /><br /><hr></hr><br /><em>Monthly Passes:</em><br />" & strFullFare & "  - Full Fare<br />" & strReducedFare & " - Reduced Fare<br /><br /><em>for the month of </em>" & strMonth & "<br /><hr></hr><br /><em>PAL Passes:</em><br />" & strPal10 & " - 10 trip PAL<br />" & strPal20 & " - 20 trip PAL<br /><br /><br />User Agent: " & Request.UserAgent & "<br />IP Address: " & Request.UserHostAddress

        Dim myMail As New System.Net.Mail.MailMessage("pr@niagarafrontiertransportationauthority.com", mailTo, mailSubject, mailBody)
        myMail.Bcc.Add("corey_hacker@nfta.com")
        myMail.Bcc.Add("pr@nfta.com")
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