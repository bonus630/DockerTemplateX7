Imports Corel.Interop.VGCore
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Runtime.InteropServices

Namespace _
    Public Class DataSourceFactory
        Implements ICUIDataSourceFactory

    Private DataSourceList As Dictionary(Of String, Type) = New Dictionary(Of String, Type)()

        Public Sub AddDataSource(ByVal name As String, ByVal dataSource As Type)
            DataSourceList.Add(name, dataSource)
        End Sub

        Public Sub Register()
            For Each name As String In DataSourceList.Keys
                ControlUI.corelApp.FrameWork.Application.RegisterDataSource(name, Me)
            Next
        End Sub

        Public Sub CreateDataSource(ByVal DataSourceName As String, ByVal Proxy As DataSourceProxy, <Out> ByRef ppVal As Object)
            If DataSourceList.ContainsKey(DataSourceName) Then
                Dim type As Type = DataSourceList(DataSourceName)
                ppVal = type.Assembly.CreateInstance(type.FullName, True, System.Reflection.BindingFlags.CreateInstance, Nothing, New Object() {Proxy}, Nothing, Nothing)
                Return
            End If

            ppVal = Nothing
        End Sub

        Public Function CreateDataSource(ByVal DataSourceName As String, ByVal Proxy As DataSourceProxy) As Object
            Dim ppVal As Object = Nothing

            If DataSourceList.ContainsKey(DataSourceName) Then
                Dim type As Type = DataSourceList(DataSourceName)
                ppVal = type.Assembly.CreateInstance(type.FullName, True, System.Reflection.BindingFlags.CreateInstance, Nothing, New Object() {Proxy}, Nothing, Nothing)
            End If

            Return ppVal
        End Function
    End Class
End Namespace
