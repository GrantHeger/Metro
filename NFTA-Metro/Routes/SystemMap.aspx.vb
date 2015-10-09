Imports NFTA_RouteDB
Imports System.Linq
Imports System

Partial Public Class _SM
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SetDynamicContent()

        End If
    End Sub

    Private Sub SetDynamicContent()
        Dim myRouteObject As RouteBuilder = RouteBuilder.GetInstance
        'ClientScript.RegisterClientScriptBlock(Me.GetType, "routeArrays", myRouteObject.Script)

        Me.litMarkup.Text = myRouteObject.Markup
        Me.litRouteScript.Text = myRouteObject.Script
        Me.litTrainMarkup.Text = myRouteObject.TrainMarkup

    End Sub

End Class