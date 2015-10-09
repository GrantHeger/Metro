Partial Public Class Schedules
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetMainCategories()

    End Sub

    Protected Sub GetMainCategories()
        Dim results = nftaRoute.GetMainRouteCategories
        Dim tbl As DataTable = New DataTable
        If Not IsNothing(results) Then
            With tbl
                .Columns.Add("Category_Id", GetType(Integer))
                .Columns.Add("CategoryName", GetType(String))
            End With
            Dim rw As DataRow
            For Each result In results
                rw = tbl.NewRow
                rw.Item("Category_Id") = result.Category_Id
                rw.Item("CategoryName") = result.CategoryName
                tbl.Rows.Add(rw)
            Next
        End If
        rptCategories.DataSource = tbl
        rptCategories.DataBind()
    End Sub

    Private Sub rptCategories_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptCategories.ItemDataBound
        Dim rpt As Repeater = e.Item.FindControl("rptRoutes")
        Dim results = nftaRoute.GetRoutes(e.Item.DataItem(0))
        Dim tbl As DataTable = New DataTable
        If Not IsNothing(results) Then
            With tbl
                .Columns.Add("RouteId", GetType(Integer))
                .Columns.Add("RouteName", GetType(String))
                .Columns.Add("RouteNumber", GetType(String))
                .Columns.Add("StatusColor", GetType(Integer))
                .Columns.Add("RouteDisplayNumber", GetType(String))
                .Columns.Add("Rev", GetType(String))
                .Columns.Add("Delta", GetType(String))
            End With
            Dim rw As DataRow
            For Each result In results
                rw = tbl.NewRow
                rw.Item("RouteId") = result.RouteId
                rw.Item("RouteName") = result.RouteName
                rw.Item("RouteNumber") = result.RouteNumber
                rw.Item("StatusColor") = result.StatusColor
                rw.Item("RouteDisplayNumber") = If(Not result.RouteNumber = "45", "<em>#" & result.RouteNumber & "</em>", Nothing)
                If result.RouteNumber = "56" Then
                    rw.Item("RouteDisplayNumber") = "<em>#55T</em>"
                End If
                rw.Item("Rev") = If(Not String.IsNullOrEmpty(result.Revised), _
                    result.Revised, Nothing)

                'rw.Item("Delta") = "<img src='/img/delta.png' alt='Upcoming " & result.Preview & " route change' width='20' />"
                rw.Item("Delta") = If(Not String.IsNullOrEmpty(result.Preview), _
                "<img src='/img/delta.png' alt='Upcoming " & result.Preview & " route change' width='20' />", Nothing)
                
                tbl.Rows.Add(rw)
            Next
        End If
        rpt.DataSource = tbl
        rpt.DataBind()
    End Sub
End Class