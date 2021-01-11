Imports System.Windows
Imports Corel.Interop.VGCore

Public Class ControlUI
    Public Shared corelApp As Corel.Interop.VGCore.Application

    Sub New(ByVal corelApp As Object)
        Try
            Me.corelApp = TryCast(corelApp, Corel.Interop.VGCore.Application)
            Dim dsf = New DataSource.DataSourceFactory()
            dsf.AddDataSource("$safeprojectname$DS", GetType(DataSource.$safeprojectname$DataSource))
            dsf.Register()
        Catch
            Global.System.Windows.MessageBox.Show("VGCore Erro")
        End Try

    End Sub

End Class
