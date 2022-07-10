Imports Corel.Interop.VGCore

Public Class Addon
    Sub New(ByVal application As Application)
        ' Create And register our tool
        Dim toolState As ToolState
        toolState = New $safeprojectname$(application)
        application.RegisterToolState("$GuidA$", "$safeprojectname$", toolState)
    End Sub
End Class
