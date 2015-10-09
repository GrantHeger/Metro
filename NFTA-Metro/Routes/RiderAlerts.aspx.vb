Partial Public Class _RiderAlerts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("/Routes/MetroNow.aspx")
        GetData()
    End Sub

    Protected Sub GetData()
        Dim tbl As DataTable = New DataTable
        With tbl
            .Columns.Add("CategoryId", GetType(Integer))
            .Columns.Add("CategoryName", GetType(String))
        End With
        Dim cats() = {2, 5}
        For Each cat In cats
            Dim results As DataTable = NFTAAlert.GetDistinctActiveAlertSubCategoryNames(cat)
            If Not IsNothing(results) Then
                Dim rw As DataRow
                For Each result In results.Rows
                    rw = tbl.NewRow
                    rw.Item("CategoryId") = result.item("Category_Id")
                    rw.Item("CategoryName") = result.item("CategoryName")
                    tbl.Rows.Add(rw)
                Next
            End If
        Next
        rptCategories.DataSource = tbl
        rptCategories.DataBind()
        phNoAlerts.Visible = If(rptCategories.Items.Count > 0, False, True)
    End Sub

    Private Sub rptCategories_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptCategories.ItemDataBound
        Dim rpt As Repeater = e.Item.FindControl("rptAlerts")
        rpt.DataSource = NFTAAlert.GetAlertsBySubCategory(e.Item.DataItem(0))
        rpt.DataBind()
    End Sub

End Class