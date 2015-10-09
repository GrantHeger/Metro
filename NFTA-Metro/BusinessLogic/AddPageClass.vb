Imports System.IO

Public Class AddPageClass

    Public Shared Function AddClass(ByVal pg As String) As String
        Select Case Split(pg.ToString, "\")(UBound(Split(pg.ToString, "\"))).ToString.ToLower
            Case "about"
                Return "pageAbout"
            Case "alerts"
                Return "pageAlerts"
            Case "routes"
                Return "pageRoutes"
            Case "programs"
                Return "pagePrograms"
            Case "tips"
                Return "pageTips"
            Case "paratransit"
                Return "pageParatransit"
            Case "trip"
                Return "pageTripPlanner"
            Case "contact"
                Return "pageContact"
            Case Else
                Return "pageHome"
        End Select
    End Function

End Class
