Imports System.Data.SqlClient

Public Class CACBase

    Public CommitteeId As Integer
    Public CommitteeUserId As String
    Public CommitteeUsername As String
    Public FirstName As String
    Public LastName As String
    Public Address As String
    Public City As String
    Public State As String
    Public Zip As String
    Public County As String
    Public Phone As String
    Public Email As String
    Public Statement As String
    Public IsActive As Boolean
    Public Comments As String
    Public DateAdded As DateTime
    Public DateModified As DateTime

    Public Sub Initialize(ByVal cacId As Integer)
        Dim command As SqlCommand = New SqlCommand("spLocal_CAC_Get")
        command.Parameters.AddWithValue("@cacId", cacId)
        command.Parameters.AddWithValue("@isActive", DBNull.Value)
        command.Parameters.AddWithValue("@last", DBNull.Value)
        Dim tbl As DataTable = StoredProcedureReader.Read(command).Tables(0)
        If Not IsNothing(tbl) Then
            Dim rw As DataRow = tbl.Rows(0)
            CommitteeId = Integer.Parse(rw.Item("cacId"))
            CommitteeUserId = rw.Item("UserId").ToString
            CommitteeUsername = rw.Item("UserName").ToString
            FirstName = rw.Item("FirstName").ToString
            LastName = rw.Item("LastName").ToString
            Address = rw.Item("Address").ToString
            City = rw.Item("City").ToString
            State = rw.Item("State").ToString
            Zip = rw.Item("Zip").ToString
            County = rw.Item("County").ToString
            Phone = rw.Item("Phone").ToString
            Email = rw.Item("Email").ToString
            Comments = rw.Item("Comments").ToString
            DateAdded = Date.Parse(rw.Item("DateAdded").ToString)
            If Not String.IsNullOrEmpty(rw.Item("DateModified")) Then DateModified = Date.Parse(rw.Item("DateModified").ToString)
        End If
    End Sub

    Public Function Create(ByVal cb As CACBase) As Integer
        Dim command As SqlCommand = New SqlCommand("spLocal_CAC_Add")
        command.Parameters.AddWithValue("@first", cb.FirstName)
        command.Parameters.AddWithValue("@last", cb.LastName)
        command.Parameters.AddWithValue("@userId", cb.CommitteeUserId)
        command.Parameters.AddWithValue("@address", cb.Address)
        command.Parameters.AddWithValue("@city", cb.City)
        command.Parameters.AddWithValue("@state", cb.State)
        command.Parameters.AddWithValue("@zip", cb.Zip)
        command.Parameters.AddWithValue("@county", cb.County)
        command.Parameters.AddWithValue("@phone", cb.Phone)
        command.Parameters.AddWithValue("@statement", cb.Statement)
        command.Parameters.AddWithValue("@isActive", True)
        command.Parameters.AddWithValue("@comments", cb.Comments)
        Command.Parameters.Add("@pRecordId", SqlDbType.Int).Direction = ParameterDirection.Output
        Dim newId As String = StoredProcedureReader.ReadOutput(Command).Parameters("@pRecordId").Value.ToString
        Return If(Not String.IsNullOrEmpty(newId), Integer.Parse(newId), 0)
    End Function

    Public Sub Update(ByVal cb As CACBase)
        Dim command As SqlCommand = New SqlCommand("spLocal_CAC_Update")
        command.Parameters.AddWithValue("@cacId", cb.CommitteeId)
        command.Parameters.AddWithValue("@first", cb.FirstName)
        command.Parameters.AddWithValue("@last", cb.LastName)
        command.Parameters.AddWithValue("@userId", cb.CommitteeUserId)
        command.Parameters.AddWithValue("@address", cb.Address)
        command.Parameters.AddWithValue("@city", cb.City)
        command.Parameters.AddWithValue("@state", cb.State)
        command.Parameters.AddWithValue("@zip", cb.Zip)
        command.Parameters.AddWithValue("@county", cb.County)
        command.Parameters.AddWithValue("@phone", cb.Phone)
        command.Parameters.AddWithValue("@statement", cb.Statement)
        command.Parameters.AddWithValue("@isActive", cb.IsActive)
        command.Parameters.AddWithValue("@comments", cb.Comments)
        StoredProcedureReader.Read(command)
    End Sub

    Public Sub Delete(ByVal cacId As Integer)
        Dim command As SqlCommand = New SqlCommand("spLocal_CAC_Delete")
        command.Parameters.AddWithValue("@cacId", cacId)
        StoredProcedureReader.Read(command)
    End Sub

    Public Function GetCACMembers(ByVal cacId As Integer, ByVal active As Integer, ByVal lname As String) As DataTable
        Dim command As SqlCommand = New SqlCommand("spLocal_CAC_Get")
        If cacId > 0 Then command.Parameters.AddWithValue("@cacId", cacId) Else command.Parameters.AddWithValue("@cacId", DBNull.Value)
        If active > -1 Then command.Parameters.AddWithValue("@isActive", If(active = 0, False, True)) Else command.Parameters.AddWithValue("@isActive", DBNull.Value)
        If Not String.IsNullOrEmpty(lname) Then command.Parameters.AddWithValue("@last", lname) Else command.Parameters.AddWithValue("@last", DBNull.Value)
        Return StoredProcedureReader.Read(command).Tables(0)
    End Function

    Public Sub AddCACRoute(ByVal cacId As Integer, ByVal routeId As Integer, ByVal freq As String)
        Dim command As SqlCommand = New SqlCommand("spLocal_CAC_Route_Add")
        command.Parameters.AddWithValue("@cacId", cacId)
        command.Parameters.AddWithValue("@routeId", routeId)
        command.Parameters.AddWithValue("@freq", freq)
        StoredProcedureReader.Read(command)
    End Sub

    Public Sub UpdateCACRouteFrequency(ByVal crId As Integer, ByVal freq As String)
        Dim command As SqlCommand = New SqlCommand("spLocal_CAC_Route_Update")
        command.Parameters.AddWithValue("@crId", crId)
        command.Parameters.AddWithValue("@freq", freq)
        StoredProcedureReader.Read(command)
    End Sub

    Public Sub DeleteCACRoute(ByVal crId As Integer)
        Dim command As SqlCommand = New SqlCommand("spLocal_CAC_Route_Delete")
        command.Parameters.AddWithValue("@crId", crId)
        StoredProcedureReader.Read(command)
    End Sub

    Public Function GetCACRoutes(ByVal crId As Integer, ByVal cacId As Integer, ByVal routeId As Integer) As DataTable
        Dim command As SqlCommand = New SqlCommand("spLocal_CAC_Route_Get")
        If crId > 0 Then command.Parameters.AddWithValue("@crId", crId) Else command.Parameters.AddWithValue("@crId", DBNull.Value)
        If cacId > 0 Then command.Parameters.AddWithValue("@cacId", cacId) Else command.Parameters.AddWithValue("@cacId", DBNull.Value)
        If routeId > 0 Then command.Parameters.AddWithValue("@routeId", routeId) Else command.Parameters.AddWithValue("@routeId", DBNull.Value)
        Return StoredProcedureReader.Read(command).Tables(0)
    End Function

    Public Sub AddCACService(ByVal cacId As Integer, ByVal service As String)
        Dim command As SqlCommand = New SqlCommand("spLocal_CAC_Service_Add")
        command.Parameters.AddWithValue("@cacId", cacId)
        command.Parameters.AddWithValue("@service", service)
        StoredProcedureReader.Read(command)
    End Sub

    Public Sub DeleteCACService(ByVal csId As Integer)
        Dim command As SqlCommand = New SqlCommand("spLocal_CAC_Service_Delete")
        command.Parameters.AddWithValue("@csId", csId)
        StoredProcedureReader.Read(command)
    End Sub

    Public Function GetCACServices(ByVal csId As Integer, ByVal cacId As Integer, ByVal service As String) As DataTable
        Dim command As SqlCommand = New SqlCommand("spLocal_CAC_Service_Get")
        If csId > 0 Then command.Parameters.AddWithValue("@csId", csId) Else command.Parameters.AddWithValue("@csId", DBNull.Value)
        If cacId > 0 Then command.Parameters.AddWithValue("@cacId", cacId) Else command.Parameters.AddWithValue("@cacId", DBNull.Value)
        If Not String.IsNullOrEmpty(service) Then command.Parameters.AddWithValue("@service", service) Else command.Parameters.AddWithValue("@service", DBNull.Value)
        Return StoredProcedureReader.Read(command).Tables(0)
    End Function

    Public Function CreateUserAccount(ByVal useremail As String, ByVal pwd As String) As String
        Dim status As MembershipCreateStatus
        Dim newUser As MembershipUser
        Try
            newUser = Membership.CreateUser(useremail, pwd, useremail, Nothing, Nothing, True, status)
        Catch ex As Exception
            Return ex.Message
        End Try
        If newUser Is Nothing Then
            Return GetErrorMessage(status)
        Else
            Roles.AddUserToRole(useremail, "NFTA User")
            Return String.Empty
        End If
    End Function

    Public Function GetErrorMessage(ByVal status As MembershipCreateStatus) As String
        Select Case status
            Case MembershipCreateStatus.DuplicateUserName
                Return "The username you selected already exists. Please try a different user name."
            Case MembershipCreateStatus.DuplicateEmail
                Return "A username for that e-mail address already exists. Please enter a different e-mail address."
            Case MembershipCreateStatus.InvalidPassword
                Return "The password provided is invalid. Please enter a valid password value."
            Case MembershipCreateStatus.InvalidEmail
                Return "The e-mail address provided is invalid. Please check the value and try again."
            Case MembershipCreateStatus.InvalidAnswer
                Return "The password retrieval answer provided is invalid. Please check the value and try again."
            Case MembershipCreateStatus.InvalidQuestion
                Return "The password retrieval question provided is invalid. Please check the value and try again."
            Case MembershipCreateStatus.InvalidUserName
                Return "The user name provided is invalid. Please check the value and try again."
            Case MembershipCreateStatus.ProviderError
                Return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator."
            Case MembershipCreateStatus.UserRejected
                Return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator."
            Case Else
                Return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please call the GRWC phone number found on our <a href='/Contact.aspx' title='click here to go to our contact page'>contact page</a>."
        End Select
    End Function

    Public Function AddCACContact(ByVal userId As String, ByVal email As String) As Integer
        Dim command As SqlCommand = New SqlCommand("spLocal_CAC_Contact_Add")
        command.Parameters.AddWithValue("@userId", userId)
        command.Parameters.AddWithValue("@email", email)
        command.Parameters.Add("@pRecordId", SqlDbType.Int).Direction = ParameterDirection.Output
        Dim newId As String = StoredProcedureReader.ReadOutput(command).Parameters("@pRecordId").Value.ToString
        Return If(Not String.IsNullOrEmpty(newId), Integer.Parse(newId), 0)
        ClearCache("AlertsContacts")
    End Function

    Public Function GetUserContactId(ByVal username As String, ByVal email As String) As Integer
        Dim contactId As New Integer
        Dim svc As New AlertService.AlertService
        Dim results = svc.GetUserContacts(Membership.GetUser(username).ProviderUserKey.ToString)
        If Not IsNothing(results) Then
            For Each result In results
                If result.Contact_Info.ToLower = email.ToLower Then
                    contactId = result.Contact_Id
                    Exit For
                End If
            Next
        End If
        Return contactId
    End Function

    Public Function AddNewSubscription(ByVal contactId As Integer) As Integer
        Dim command As SqlCommand = New SqlCommand("spLocal_CAC_AlertSubscription_Add")
        command.Parameters.AddWithValue("@contactId", contactId)
        command.Parameters.AddWithValue("@catId", 166)
        command.Parameters.Add("@pRecordId", SqlDbType.Int).Direction = ParameterDirection.Output
        Dim newId As String = StoredProcedureReader.ReadOutput(command).Parameters("@pRecordId").Value.ToString
        Return If(Not String.IsNullOrEmpty(newId), Integer.Parse(newId), 0)
        ClearCache("AlertsSubscriptions")
    End Function

    Public Sub ClearCache(ByVal cachename As String)
        Dim svc As New CacheService.CacheService
        svc.ClearCache(cachename)
    End Sub

End Class
