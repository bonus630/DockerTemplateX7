Imports Corel.Interop.VGCore
Public Class $safeprojectname$ : Implements ToolState
    Private save As Point
    Private application As Application
    Private ui As OnScreenCurve
    Private currentAttribs As ToolStateAttributes
    Private _IsDrawing As Boolean

    Sub New(ByVal application As Application)
        Me.application = application 'Save the application, we use it to create the new line segment
    End Sub


    Public Sub OnStartState(StateAttributes As ToolStateAttributes) Implements IVGToolState.OnStartState
        'save the state attributes so that we can reuse it later
        currentAttribs = StateAttributes
        currentAttribs.SetCursor(cdrCursorShape.cdrCursorSmallcrosshair)
        currentAttribs.AllowTempPickState = False
        'uncomment the next line to use a propertybar
        'currentAttribs.PropertyBarGuid = "$GuidB$"
        ui = application.CreateOnScreenCurve() 'create the Xor'd UI object
    End Sub

    Public Sub OnExitState() Implements IVGToolState.OnExitState
        Reset() 'cleanup, reset UI and variables
    End Sub

    Public Sub OnMouseMove(pt As Point) Implements IVGToolState.OnMouseMove
        If (_IsDrawing) Then
            'update the Xor'd line
            ui.SetLine(save.x, save.y, pt.x, pt.y)
            ui.Show()
        End If
    End Sub

    Public Sub OnLButtonDown(pt As Point) Implements IVGToolState.OnLButtonDown
        Throw New NotImplementedException()
    End Sub

    Public Sub OnLButtonDownLeaveGrace(pt As Point) Implements IVGToolState.OnLButtonDownLeaveGrace
        save = pt         'save the point from the left mouse down.
        _IsDrawing = True 'place us into the drawing substate
    End Sub

    Public Sub OnLButtonUp(pt As Point) Implements IVGToolState.OnLButtonUp
        OnCommit(pt) ' forward to OnCommit, which will create the line segment
    End Sub

    Public Sub OnLButtonDblClick(pt As Point) Implements IVGToolState.OnLButtonDblClick
        Throw New NotImplementedException()
    End Sub

    Public Sub OnClick(pt As Point, ByRef Handled As Boolean) Implements IVGToolState.OnClick
        Throw New NotImplementedException()
    End Sub

    Public Sub OnRButtonDown(pt As Point, ByRef Handled As Boolean) Implements IVGToolState.OnRButtonDown
        Throw New NotImplementedException()
    End Sub

    Public Sub OnRButtonUp(pt As Point, ByRef Handled As Boolean) Implements IVGToolState.OnRButtonUp
        Throw New NotImplementedException()
    End Sub

    Public Sub OnKeyDown(KeyCode As Integer, ByRef Handled As Boolean) Implements IVGToolState.OnKeyDown
        Throw New NotImplementedException()
    End Sub

    Public Sub OnKeyUp(KeyCode As Integer, ByRef Handled As Boolean) Implements IVGToolState.OnKeyUp
        Throw New NotImplementedException()
    End Sub

    Public Sub OnDelete(ByRef Handled As Boolean) Implements IVGToolState.OnDelete
        Throw New NotImplementedException()
    End Sub

    Public Sub OnAbort() Implements IVGToolState.OnAbort
        Reset() 'cleanup, reset UI and variables
    End Sub

    Public Sub OnCommit(pt As Point) Implements IVGToolState.OnCommit
        If (_IsDrawing) Then
            Reset() 'cleanup And reset UI And variable
            application.ActiveDocument.ActiveLayer.CreateLineSegment(save.x, save.y, pt.x, pt.y) 'create the Object
        End If



    End Sub

    Public Sub OnSnapMouse(pt As Point, ByRef Handled As Boolean) Implements IVGToolState.OnSnapMouse
        Handled = IsDrawing ' use the Default snapping If we are Not drawing
        If (IsDrawing) Then
            ' if we are drawing, we would Like to use the anchored snapping, this will allow the perpendicular And tangent
            ' snapping to kick in as well.
            currentAttribs.AnchoredSnapMouse(pt, save)
            ' also, we'd like to use the built-in constaints (ctrl key to constrain to 15 degree increments)
            currentAttribs.ConstrainMouse(pt, save)
        End If
    End Sub

    Public Sub OnTimer(TimerId As Integer, TimeEllapsed As Integer) Implements IVGToolState.OnTimer
        Throw New NotImplementedException()
    End Sub

    Public ReadOnly Property IsDrawing As Boolean Implements IVGToolState.IsDrawing
        Get
            Return _IsDrawing
        End Get
    End Property
    Private Sub Reset()
        ui.Hide()
        _IsDrawing = False
    End Sub
End Class
