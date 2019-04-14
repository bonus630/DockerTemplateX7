Imports System.Windows
Imports Corel.Interop.VGCore
Public Class StylesController
    Private WithEvents corelApp As Corel.Interop.VGCore.Application
    Private Resources As ResourceDictionary
    Private currentTheme As String
    Public Sub New(resource As ResourceDictionary, app As Corel.Interop.VGCore.Application)
        Resources = resource
        corelApp = app
    End Sub
#Region "theme Select"
    'Keys resources name follow the resource order to add a New value, order to works you need add 5 resources colors And Resources/Colors.xaml
    '1º Is default, Is the same name of StyleKeys string array
    '2º add LightestGrey. in start name of 1º for LightestGrey style in corel
    '3º MediumGrey
    '4º DarkGrey
    '5º Black

    Private ReadOnly StyleKeys() As String = {
         "Button.MouseOver.Background",
         "Button.MouseOver.Border",
         "Button.Static.Border",
         "Button.Static.Background",
         "Button.Pressed.Background",
         "Button.Pressed.Border",
         "Button.Disabled.Foreground",
         "Button.Disabled.Background",
         "Default.Static.Foreground",
         "Default.Static.Background",
         "Container.Text.Static.Background",
         "Container.Text.Static.Foreground",
         "Container.Static.Background",
         "Default.Static.Inverted.Foreground",
         "ComboBox.Border.Popup.Item.MouseOver"
  }


    Private Sub LoadStyle(name As String)
        Dim style As String
        style = name.Substring(name.LastIndexOf("_") + 1)
        For i As Integer = 0 To StyleKeys.Length
            Me.Resources(StyleKeys(i)) = Me.Resources(String.Format("{0}.{1}", style, StyleKeys(i)))
        Next
    End Sub

    Private Sub CorelApp_OnApplicationEvent(EventName As String, ByRef Parameters As Object()) Handles corelApp.OnApplicationEvent
        If EventName.Equals("WorkspaceChanged") = True OrElse EventName.Equals("OnColorSchemeChanged") = True Then
            LoadThemeFromPreference()
        End If
    End Sub

    Public Sub LoadThemeFromPreference()
        Try
            Dim result As String = String.Empty

#If X8 Then
                 result = corelApp.GetApplicationPreferenceValue("WindowScheme", "Colors").ToString()
#End If
#If X9 Then
                  result = corelApp.GetApplicationPreferenceValue("WindowScheme", "Colors").ToString()
#End If
#If X10 Then
                  result = corelApp.GetApplicationPreferenceValue("WindowScheme", "Colors").ToString()
#End If
#If X11 Then
                  result = corelApp.GetApplicationPreferenceValue("WindowScheme", "Colors").ToString()
#End If
            If Not result.Equals(currentTheme) Then
                If Not result.Equals(String.Empty) Then
                    currentTheme = result
                    LoadStyle(currentTheme)
                End If
            End If

        Catch
        End Try


    End Sub

#End Region
End Class
