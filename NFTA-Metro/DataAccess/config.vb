Module config

    Public CurrentSchedule As String '= ConfigurationManager.AppSettings("CurrentSchedule").ToString
    Public NextSchedule As String '= ConfigurationManager.AppSettings("NextSchedule").ToString
    Public CurrentScheduleFolder As String '= ConfigurationManager.AppSettings("CurrentScheduleFolder").ToString
    Public NextScheduleFolder As String '= ConfigurationManager.AppSettings("NextScheduleFolder").ToString
    Public ShowNextSchedule As Boolean '= ConfigurationManager.AppSettings("ShowNextSchedule")

    Public Sub SetConfigVariables()
        Dim svc As New MetroService.MetroService
        Dim results = svc.GetMetroConfigItems()
        If Not IsNothing(results) Then
            For Each result In results
                If result.key.ToString.ToLower = "currentschedule" Then CurrentSchedule = result.value
                If result.key.ToString.ToLower = "nextschedule" Then NextSchedule = result.value
                If result.key.ToString.ToLower = "currentschedulefolder" Then CurrentScheduleFolder = result.value
                If result.key.ToString.ToLower = "nextschedulefolder" Then NextScheduleFolder = result.value
                If result.key.ToString.ToLower = "shownextschedule" Then ShowNextSchedule = Boolean.Parse(result.value)
            Next
        End If
    End Sub

End Module
