Partial Public Class _Escalator
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillStatus()
    End Sub


    Protected Sub FillStatus()
        Dim results As DataTable = NFTAEscalator.GetEscalatorStatus
        Dim tbl As New DataTable
        If Not IsNothing(tbl) Then
            With tbl
                .Columns.Add("id", GetType(Integer))
                .Columns.Add("Station", GetType(String))
                .Columns.Add("Device", GetType(String))
                .Columns.Add("Direction", GetType(String))
                .Columns.Add("Status", GetType(String))
                .Columns.Add("LastModified", GetType(String))
                .Columns.Add("Type", GetType(String))
                .Columns.Add("Icon", GetType(String))
                .Columns.Add("Combo", GetType(String))
            End With
            Dim rw As DataRow
            For Each result In results.Rows
                rw = tbl.NewRow
                rw.Item("id") = result.item("Id")
                rw.Item("Station") = result.item("Station")
                rw.Item("Device") = result.item("Device")
                rw.Item("Direction") = result.item("Direction")
                If result.item("Type") = "Escalator" Then
                    If result.item("Direction") = "Up" Then
                        rw.Item("Icon") = "<img src='/img/icon-esc-up.gif' width='30' height='30' alt='Up Escalator'/>"
                        rw.Item("Combo") = result.item("Direction") & " " & result.item("Type") & " " & result.item("Device")
                    Else
                        rw.Item("Icon") = "<img src='/img/icon-esc-down.gif' width='30' height='30' alt='Down Escalator'/>"
                        rw.Item("Combo") = result.item("Direction") & " " & result.item("Type") & " " & result.item("Device")
                    End If

                Else
                    'result.item("Type") = "Elevator"
                    rw.Item("Icon") = "<img src='/img/icon-ele.gif' width='30' height='30' alt='Elevator'/>"
                    rw.Item("Combo") = result.item("Type") & " " & result.item("Device")
                End If
                rw.Item("Status") = If(result.item("Status") = "Not Running", "<img src='/img/icon-stop.gif' width='30' height='30' alt='Not Running' /><br />" & result.item("Status"), "<img src='/img/icon-check.gif' width='30' height='30' alt='Not Running' /><br />" & result.item("Status"))
                rw.Item("LastModified") = result.item("LastModified")
                tbl.Rows.Add(rw)
            Next
        End If
        rptEscalator.DataSource = tbl
        rptEscalator.DataBind()
    End Sub

End Class