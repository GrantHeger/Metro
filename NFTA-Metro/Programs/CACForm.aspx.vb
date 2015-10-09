Partial Public Class _cac
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            FillStateDropDown()
            FillCategories()
            txtFirstName.Focus()
            ddlState.SelectedValue = "NY"
        End If
    End Sub

    Protected Sub FillStateDropDown()
        Dim svc As New StatesAndProvincesService.StatesAndProvincesService
        ddlState.DataSource = svc.GetUSStates
        ddlState.DataBind()
        ddlState.Items.Insert(0, New ListItem("Select State", "0"))
    End Sub

    Protected Sub FillCategories()
        Dim results As DataTable = nftaRoute.GetRouteCategorieWithActiveRoutes
        Dim tbl As DataTable = New DataTable
        If Not IsNothing(results) Then
            With tbl
                .Columns.Add("Category_Id", GetType(Integer))
                .Columns.Add("CategoryName", GetType(String))
                .Columns.Add("Count", GetType(Integer))
            End With
            Dim rw As DataRow
            For Each result In results.Rows
                rw = tbl.NewRow
                rw.Item("Category_Id") = result.item("Category_Id")
                rw.Item("CategoryName") = result.item("CategoryName")
                rw.Item("Count") = result.item("Count")
                tbl.Rows.Add(rw)
            Next
        End If
        rptCategories.DataSource = tbl
        rptCategories.DataBind()
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        'server side validation
        Dim errorlist As StringBuilder = New StringBuilder
        If String.IsNullOrEmpty(txtFirstName.Text.Trim) Then errorlist.Append("<li>Please provide your first name</li>")
        If String.IsNullOrEmpty(txtLastName.Text.Trim) Then errorlist.Append("<li>Please provide your last name</li>")
        If String.IsNullOrEmpty(txtAddress.Text.Trim) Then errorlist.Append("<li>Please provide your address</li>")
        If String.IsNullOrEmpty(txtCity.Text.Trim) Then errorlist.Append("<li>Please provide your city</li>")
        If ddlState.SelectedValue = "0" Then errorlist.Append("<li>Please provide your state</li>")
        If String.IsNullOrEmpty(txtZip.Text.Trim) Then errorlist.Append("<li>Please provide your zip</li>")
        If String.IsNullOrEmpty(txtCounty.Text.Trim) Then errorlist.Append("<li>Please provide your county</li>")
        If String.IsNullOrEmpty(txtPhone.Text.Trim) Then errorlist.Append("<li>Please provide your phone #</li>")
        If String.IsNullOrEmpty(txtEmail.Text.Trim) Then errorlist.Append("<li>Please provide your email</li>")
        If String.IsNullOrEmpty(txtStatement.Text.Trim) Then errorlist.Append("<li>Please provide your statement</li>")
        If cbAgree.Checked = False Then errorlist.Append("<li>Please read and aggree to the term and conditions</li>")
        Dim blnServicesChecked As Boolean = False
        Dim services As New ArrayList
        If Not String.IsNullOrEmpty(txtOther.Text.Trim) Then
            blnServicesChecked = True
            services.Add(txtOther.Text.Trim)
        End If
        For Each cb As CheckBox In GetServiceCheckboxArray()
            If cb.Checked Then
                services.Add(cb.Text.Trim)
                blnServicesChecked = True
            End If
        Next
        If blnServicesChecked = False Then errorlist.Append("<li>Please ensure you select at least one of our services</li>")
        Dim routesandfrequencies() As ArrayList = GetRouteInfo()
        If routesandfrequencies.Length = 2 Then
            If routesandfrequencies(0).Count = 0 Then errorlist.Append("<li>Please ensure you select at least one route that you use</li>")
            If routesandfrequencies(0).Count <> routesandfrequencies(1).Count Then errorlist.Append("<li>Please ensure you've selected the frequency for the route(s) you checked</li>")
            If routesandfrequencies(1).Count = 0 Then errorlist.Append("<li>Please ensure you've selected the frequency for the route(s) you checked</li>")
        Else
            errorlist.Append("<li>Please ensure you've selected the routes you use and the frequency of your use for each</li>")
        End If

        If String.IsNullOrEmpty(errorlist.ToString) Then
            Dim cacb As CACBase = New CACBase
            Dim isNewUser As Boolean = True
            Dim pwd As String = String.Empty

            'first see if this is a non-email using person
            If txtEmail.Text.Trim.ToLower = "pr@nfta.com" Then
                'only add to the CAC table; forget about membership and an alert subscription
                AddCACRecord(String.Empty, services, routesandfrequencies, False)
                phResponse.Visible = True
                phForm.Visible = False
            Else
                'see if this person already exists as a member
                Dim alertusername As String = Membership.GetUserNameByEmail(txtEmail.Text.Trim)
                If String.IsNullOrEmpty(alertusername) Then
                    'we're going to use their email address as their username
                    alertusername = txtEmail.Text.Trim
                    'create a password for the user
                    pwd = Membership.GeneratePassword(8, 1)
                    If cacb.CreateUserAccount(txtEmail.Text.Trim, pwd) = String.Empty Then
                    Else
                        'there was some issue creating this account: display the error message back to the applicant
                        isNewUser = False
                        phErrors.Visible = True
                        litErrors.Text = errorlist.ToString
                    End If
                Else
                    isNewUser = False
                End If
                'add the applicant to the CAC table along with their selected 
                'services and routes to the respective CAC_Service table and CAC_Route table
                AddCACRecord(alertusername, services, routesandfrequencies, True)

                If isNewUser Then   'we need to add them to the Contact table
                    AddContactRecord(cacb, alertusername)   'this method also adds the subscription to the Committee's Instant Update category
                    'inform the applicant of their username and password for future Instant Update logins
                    litNewUserEmail.Text = txtEmail.Text.Trim
                    litNewUserPassword.Text = pwd
                    phNewUser.Visible = True
                Else    'get their existing Contact Id
                    Dim contactId As Integer = cacb.GetUserContactId(alertusername, txtEmail.Text.Trim)
                    'add a subscription to the Committee's Instant Update category
                    cacb.AddNewSubscription(contactId)
                    phExistingUser.Visible = True
                End If
                phResponse.Visible = True
                phForm.Visible = False
            End If
        Else
            phErrors.Visible = True
            litErrors.Text = errorlist.ToString
            btnSubmit.Focus()
        End If
    End Sub

    Protected Sub AddCACRecord(ByVal username As String, ByVal services As ArrayList, ByVal RoutesAndFrequencies() As ArrayList, ByVal hasEmail As Boolean)
        Dim cacb As CACBase = New CACBase
        If hasEmail = True Then
            cacb.CommitteeUserId = Membership.GetUser(username).ProviderUserKey.ToString
        Else
            Dim g As Guid = Guid.NewGuid()
            cacb.CommitteeUserId = g.ToString
        End If
        cacb.FirstName = txtFirstName.Text.Trim
        cacb.LastName = txtLastName.Text.Trim
        cacb.Address = txtAddress.Text.Trim
        cacb.City = txtCity.Text.Trim
        cacb.State = ddlState.SelectedValue
        cacb.Zip = txtZip.Text.Trim
        cacb.County = txtCounty.Text.Trim
        cacb.Phone = txtPhone.Text.Trim
        cacb.Statement = txtStatement.Text.Trim
        'creates the new CAC record and returns the Id
        Dim newMemberId As Integer = cacb.Create(cacb)
        'cycle through all the service selections and add records for each associating them to the new CAC member's Id
        For Each svc In services
            cacb.AddCACService(newMemberId, svc)
        Next
        'cycle through all the route selections and add records for each associating them to the new CAC member's Id
        Dim routes As ArrayList = RoutesAndFrequencies(0)
        Dim frequencies As ArrayList = RoutesAndFrequencies(1)
        For i As Integer = 0 To RoutesAndFrequencies(0).Count - 1
            cacb.AddCACRoute(newMemberId, Integer.Parse(routes(i)), frequencies(i))
        Next
    End Sub

    Protected Function GetServiceCheckboxArray() As CheckBox()
        Dim serviceCheckboxArray As CheckBox() = {chkPal, chkExpress, chkFixed, chkMonthly, chkPark}
        Return serviceCheckboxArray
    End Function

    Protected Function GetRouteInfo() As ArrayList()    'returns two array lists of the selected routes and their frequencies
        Dim routesandfrequencies(1) As ArrayList
        Dim selectedroutes As New ArrayList
        Dim selectedfrequencies As New ArrayList
        For Each itm In rptCategories.Items
            Dim gv As GridView = itm.FindControl("gvRoutes")
            For Each r In gv.Rows
                Dim blnRouteSelected As Boolean = False
                For Each c In r.cells
                    For Each ctrl In c.controls
                        If TypeOf (ctrl) Is CheckBox Then
                            If TryCast(ctrl, CheckBox).Checked And TryCast(ctrl, CheckBox).ID.StartsWith("RowLevel") Then
                                blnRouteSelected = True
                                selectedroutes.Add(r.Cells(0).Text)
                            End If
                        End If
                        If blnRouteSelected = True Then
                            If TypeOf (ctrl) Is RadioButton Then
                                If TryCast(ctrl, RadioButton).Checked And TryCast(ctrl, RadioButton).ID.StartsWith("rb") Then
                                    selectedfrequencies.Add(TryCast(ctrl, RadioButton).Text.ToString)
                                End If
                            End If
                        End If
                    Next
                Next
            Next
        Next
        routesandfrequencies(0) = selectedroutes
        routesandfrequencies(1) = selectedfrequencies
        Return routesandfrequencies
    End Function

    Protected Sub AddContactRecord(ByVal cacb As CACBase, ByVal alertusername As String)
        'add applicant to Contact table
        'NOTE: web service does not return the Id and we need that, so a new sp was created for this purpose
        Dim newContactId As Integer = cacb.AddCACContact(Membership.GetUser(alertusername).ProviderUserKey.ToString, txtEmail.Text.Trim)
        'add an alert subscription for the applicant for the Committee Instant Update alert
        cacb.AddNewSubscription(newContactId)
    End Sub

    Private Sub rptCategories_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptCategories.ItemDataBound
        Dim gv As GridView = e.Item.FindControl("gvRoutes")
        gv.DataSource = nftaRoute.GetRoutes(e.Item.DataItem(0))
        gv.DataBind()
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'hide the column with the Route Id
        For Each itm In rptCategories.Items
            Dim gv As GridView = itm.FindControl("gvRoutes")
            gv.Columns(0).Visible = False
        Next
    End Sub

End Class