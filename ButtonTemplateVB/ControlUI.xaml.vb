Imports System.Windows
Imports Corel.Interop.VGCore

Public Class ControlUI
    Private corelApp As Corel.Interop.VGCore.Application
    Private stylesController As StylesController

    Sub New(ByVal corelApp As Object)
        InitializeComponent()
        Try
            Me.corelApp = TryCast(corelApp, Corel.Interop.VGCore.Application)
            Me.stylesController = New StylesController(Me.Resources, Me.corelApp)
        Catch
            Global.System.Windows.MessageBox.Show("VGCore Erro")
        End Try

    End Sub
    Protected Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)
        stylesController.LoadThemeFromPreference()
    End Sub
End Class
