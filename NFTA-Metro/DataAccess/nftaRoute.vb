Imports System.Data.SqlClient

Public Class nftaRoute

    Public Shared Function GetMainRouteCategories() As List(Of RouteCategoryView)
        Dim alertdb As nftaDataContext = New nftaDataContext
        Dim myResult As List(Of RouteCategoryView) = (From r In alertdb.RouteCategoryViews.Cast(Of RouteCategoryView)() Order By r.SortOrder).ToList
        Return If(Not IsNothing(myResult) And myResult.Count > 0, myResult, Nothing)
    End Function

    Public Shared Function GetRoutes(ByVal catId As Integer) As List(Of RouteView)
        Dim alertdb As nftaDataContext = New nftaDataContext
        Dim myResult As List(Of RouteView) = (From r In alertdb.RouteViews.Cast(Of RouteView)() Where r.Category_Id = catId Order By r.CategoryName).ToList
        Return If(Not IsNothing(myResult) And myResult.Count > 0, myResult, Nothing)
    End Function

    Public Function GetRouteById(ByVal rId As Integer) As String()
        Dim alertdb As nftaDataContext = New nftaDataContext
        Dim myResult = (From r In alertdb.RouteViews.Cast(Of RouteView)() Where r.RouteNumber = rId).FirstOrDefault
        If Not IsNothing(myResult) Then
            Dim routeDetails(14) As String
            routeDetails(0) = myResult.RouteId
            routeDetails(1) = myResult.Category_Id
            routeDetails(2) = myResult.RouteName
            routeDetails(3) = myResult.RouteNumber
            routeDetails(4) = myResult.Status
            routeDetails(5) = myResult.Copy
            routeDetails(6) = myResult.Preview
            routeDetails(7) = myResult.English
            routeDetails(8) = myResult.Pages
            routeDetails(9) = myResult.Map
            routeDetails(10) = myResult.Note
            routeDetails(11) = myResult.CategoryName
            routeDetails(12) = myResult.Revised
            routeDetails(13) = myResult.TimeTable
            routeDetails(14) = myResult.Changes
            'routeDetails(15) = myResult.StatusColor
            Return routeDetails
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function GetRouteCategorieWithActiveRoutes() As DataTable
        Dim command As SqlCommand = New SqlCommand("spLocal_RouteCategories_GetGroupedRoutes")
        Return StoredProcedureReader.Read(command).Tables(0)
    End Function

    Public Shared Function GetActiveAlerts(ByVal rId As Integer, ByVal isLimited As Integer) As DataTable
        'Public Shared Function GetActiveAlerts(ByVal rId As Integer, ByVal isLimited As Integer) As DataTable
        Dim command As SqlCommand = New SqlCommand("nfta_GetRecentAlerts")
        'Changed the SqlCommand from this sp:  spLocal_GetRouteAlerts.  
        'This method is only being called from the Routes/Route.aspx.vb page and no where else in this project
        If rId > 0 Then command.Parameters.AddWithValue("@rId", rId) Else command.Parameters.AddWithValue("@rId", System.DBNull.Value)
        If isLimited > 0 Then command.Parameters.AddWithValue("@isLimited", True) Else command.Parameters.AddWithValue("@isLimited", False)
        Return StoredProcedureReader.Read(command).Tables(0)
    End Function

    Public Shared Function MetroNowAlerts(ByVal rId As Integer, ByVal isLimited As Integer) As DataTable
        'Public Shared Function GetActiveAlerts(ByVal rId As Integer, ByVal isLimited As Integer) As DataTable
        Dim command As SqlCommand = New SqlCommand("nfta_MetroNowAlerts")
        'If rId > 0 Then command.Parameters.AddWithValue("@rId", rId) Else command.Parameters.AddWithValue("@rId", System.DBNull.Value)
        'If isLimited > 0 Then command.Parameters.AddWithValue("@isLimited", True) Else command.Parameters.AddWithValue("@isLimited", False)
        Return StoredProcedureReader.Read(command).Tables(0)
    End Function

    Public Shared Function GetDistinctActiveAlerts() As DataTable
        Dim command As SqlCommand = New SqlCommand("spLocal_GetDistinctRouteAlerts")
        Return StoredProcedureReader.Read(command).Tables(0)
    End Function

    Public Shared Function GetRouteStatus() As DataTable
        Dim command As SqlCommand = New SqlCommand("spLocal_GetDistinctRouteAlerts")
        Return StoredProcedureReader.Read(command).Tables(0)
    End Function

    Public Shared Function GetCurrentRouteStatus(ByVal rId As Integer) As DataTable
        Dim command As SqlCommand = New SqlCommand("nfta_Route_GetCurrentStatus")
        If rId > 0 Then command.Parameters.AddWithValue("@routeId", rId) Else command.Parameters.AddWithValue("@routeId", System.DBNull.Value)
        Return StoredProcedureReader.Read(command).Tables(0)
    End Function

    Private Shared m_activeAlerts As DataTable

    Public Shared Property ActiveAlerts() As DataTable
        Get
            Return m_activeAlerts
        End Get
        Set(ByVal value As DataTable)
            m_activeAlerts = value
        End Set
    End Property

End Class
