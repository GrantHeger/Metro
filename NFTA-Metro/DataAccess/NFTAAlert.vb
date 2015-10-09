Imports System.Data.SqlClient

Public Class NFTAAlert

    Public Shared Function GetDistinctActiveAlertSubCategoryNames(ByVal categoryId As Integer) As DataTable  'List(Of AlertView)
        Dim command As SqlCommand = New SqlCommand("spLocal_GetDistinctActiveRouteCategories")
        command.Parameters.AddWithValue("@parentCategory", categoryId)
        Dim ds As DataSet = StoredProcedureReader.Read(command)
        Return ds.Tables(0)
    End Function

    Public Shared Function GetAlertsBySubCategory(ByVal SubCategoryId As Integer) As List(Of AlertView)
        Dim alertdb As nftaDataContext = New nftaDataContext
        Dim results As List(Of AlertView) = (From r In alertdb.AlertViews.Cast(Of AlertView)() Where r.Category_Id = SubCategoryId And r.EndDate.Value >= DateTime.Now.Date And r.DisplayAlert = True Order By r.DateAdded Descending).ToList
        Return If(Not IsNothing(results) And results.Count > 0, results, Nothing)
    End Function

    Public Function GetAlertCategoryName(ByVal categoryId As Integer) As String
        Dim alertdb As nftaDataContext = New nftaDataContext
        Return (From r In alertdb.AlertCategoryViews Where r.Category_Id = categoryId Select r.CategoryDescription).FirstOrDefault
    End Function

End Class
