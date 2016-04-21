Imports System
Imports System.Linq
Imports System.Text
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports Corel.Interop.VGCore

Public Class DockerUI
    Private corelApp As Corel.Interop.VGCore.Application

    Sub New(ByVal corelApp As Corel.Interop.VGCore.Application)
        Me.corelApp = corelApp
    End Sub
End Class
