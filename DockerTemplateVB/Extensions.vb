Imports System
Imports Corel.Interop.VGCore
Imports System.Runtime.CompilerServices

Module Extensions
    <Extension()>
    Sub BeginDraw(ByVal app As Corel.Interop.VGCore.Application, ByVal Optional commandGroup As Boolean = True, ByVal Optional optimization As Boolean = True, ByVal Optional enableEvents As Boolean = False, ByVal Optional preservSeletion As Boolean = True)
        If app.ActiveDocument IsNot Nothing Then
            If commandGroup Then app.ActiveDocument.BeginCommandGroup()
            If preservSeletion Then app.ActiveDocument.PreserveSelection = preservSeletion
        End If

        app.Optimization = optimization
        app.EventsEnabled = enableEvents
    End Sub

    <Extension()>
    Sub EndDraw(ByVal app As Corel.Interop.VGCore.Application)
        If app.ActiveDocument IsNot Nothing Then
            app.ActiveDocument.EndCommandGroup()
            app.ActiveDocument.PreserveSelection = False
        End If

        app.Optimization = False
        app.EventsEnabled = True
        app.Refresh()
    End Sub
End Module
