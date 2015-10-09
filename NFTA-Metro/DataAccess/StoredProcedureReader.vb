Imports System.Data.SqlClient

Public Class StoredProcedureReader

    Public Shared Function Read(ByRef sqlCommand As SqlCommand) As DataSet
        Dim connection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("nftaalertsConnectionString").ConnectionString)
        sqlCommand.Connection = connection
        sqlCommand.CommandType = CommandType.StoredProcedure
        connection.Open()
        Dim sqlDataAdapater As SqlDataAdapter = New SqlDataAdapter(sqlCommand)
        Dim dataSet As New DataSet()
        Try
            sqlDataAdapater.Fill(dataSet)
        Catch ex As Exception
            'LogErrors.LogError(ex.Message & ": " & ex.StackTrace, "StoredProcedureReader / Read")
            Throw
        Finally
            connection.Close()
        End Try
        Return dataSet
    End Function

    Public Shared Function ReadOutput(ByRef sqlCommand As SqlCommand) As SqlCommand
        Dim connection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("nftaalertsConnectionString").ConnectionString)
        sqlCommand.Connection = connection
        sqlCommand.CommandType = CommandType.StoredProcedure
        connection.Open()
        Try
            Dim sqlDataReader As SqlDataReader = sqlCommand.ExecuteReader()
        Catch ex As Exception
            'LogErrors.LogError(ex.Message & ": " & ex.StackTrace, "StoredProcedureReader / ReadOutput")
            Throw
        Finally
            connection.Close()
        End Try
        Return sqlCommand
    End Function

End Class
