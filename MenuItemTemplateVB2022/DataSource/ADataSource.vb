Imports System
Imports System.Collections.Generic
Imports Corel.Interop.VGCore
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Windows.Interop
Imports System.Diagnostics

Namespace DataSource
    <ComVisible(True)>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class $safeprojectname$DataSource
        Inherits BaseDataSource

        Private _caption As String = "$Caption$"
        Private _icon As String = "guid://$GuidA$"

        Public Sub New(ByVal proxy As DataSourceProxy)
            MyBase.New(proxy)
        End Sub

        Public Property Caption() As String
            Get
                Return _caption
            End Get
            Set(ByVal value As String)
                _caption = value
                NotifyPropertyChanged()
            End Set
        End Property

        Public Property Icon() As String
            Get
                Return _icon
            End Get
            Set(ByVal value As String)
                _icon = value
                NotifyPropertyChanged()
            End Set
        End Property

        Public Sub MenuItemCommand()
            ControlUI.corelApp.MsgShow("Working")
        End Sub
    End Class
End Namespace
