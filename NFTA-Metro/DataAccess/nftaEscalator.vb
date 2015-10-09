Imports System.Data.SqlClient

Public Class nftaEscalator
    'Public Shared EscUniversityDwn As String
    'Public Shared EscUniversityUp As String


    Public Shared Function GetEscalatorStatus() As DataTable
        Dim command As SqlCommand = New SqlCommand("spLocal_GetEscalatorValues")
        Return StoredProcedureReader.Read(command).Tables(0)
    End Function

    'Public Shared Function GetEscalatorStatus() As DataTable
    '    Dim command As SqlCommand = New SqlCommand("spLocal_GetEscalatorValues")
    '    Dim storedProcedureReader As New StoredProcedureReader()
    '    Dim ds As DataSet = storedProcedureReader.Read(command, "alerts")
    '    Return ds.Tables(0)
    'End Function


End Class
