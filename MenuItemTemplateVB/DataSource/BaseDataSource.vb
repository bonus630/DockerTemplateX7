Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Runtime
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text
Imports Corel.Interop.VGCore

Namespace DataSource

    <ComVisible(True)>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class BaseDataSource
        Implements INotifyPropertyChanged

        Protected m_AppProxy As DataSourceProxy

        Public Sub New(ByVal proxy As DataSourceProxy)
            Me.m_AppProxy = proxy
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler
        Private Event INotifyPropertyChanged_PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Sub NotifyPropertyChanged(
            <CallerMemberName> ByVal Optional propertyName As String = "")
            Try
                Me.m_AppProxy.UpdateListeners(propertyName)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
            Catch
            End Try
        End Sub

    End Class
