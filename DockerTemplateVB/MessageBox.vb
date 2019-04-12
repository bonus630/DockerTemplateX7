Imports System.Runtime.CompilerServices
Imports Corel.Interop.VGCore
Module MessageBox
    Public Enum DialogButtons
        Ok
        OkCancel
        AbortTryIgnor
        YesNoCancel
        YesNo
        TryAgainCancel
        CancelTryAgainContinue
        None
    End Enum
    Public Enum DialogResult
        Ok
        CancelClose
        Abort
        TryAgain
        Ignor
        Yes
        No
        TryAgainContinue
    End Enum

    ''' <summary>
    ''' Displays a message box that has a message And that returns a result.
    ''' </summary>
    ''' <param name="message">
    ''' A System.String that specifies the text to display.</param>
    ''' <returns> A MessageBoxResult value that specifies which message box button
    '''     Is clicked by the user.</returns>
    <Extension()>
    Public Function MsgShow(ByRef corelApp As Corel.Interop.VGCore.Application, message As String) As DialogResult
        Return corelApp.MsgShow(message, "", DialogButtons.Ok)
    End Function

    ''' <summary>
    ''' Displays a message box that has a message And title bar caption; And that returns
    '''    a result.
    ''' </summary>
    ''' <param name="message">A System.String that specifies the text to display.</param>
    ''' <param name="caption">A System.String that specifies the title bar caption to display.</param>
    ''' <returns>  A MessageBoxResult value that specifies which message box button
    '''     Is clicked by the user.</returns>
    <Extension()>
    Public Function MsgShow(ByRef corelApp As Corel.Interop.VGCore.Application, message As String, caption As String) As DialogResult

        Return corelApp.MsgShow(message, caption, DialogButtons.Ok)
    End Function

    ''' <summary>
    '''   Displays a message box that has a message, title bar caption, button, And icon;
    '''     And that returns a result.
    ''' </summary>
    ''' <param name="message">A System.String that specifies the text to display.</param>
    ''' <param name="caption"A System.String that specifies the title bar caption to display.></param>
    ''' <param name="buttons">A MessageBoxButton value that specifies which button Or buttons
    '''     to display.</param>
    ''' <returns>A MessageBoxResult value that specifies which message box button
    '''    Is clicked by the user.</returns>
    <Extension()>
    Public Function MsgShow(ByRef corelApp As Corel.Interop.VGCore.Application, message As String, caption As String, buttons As DialogButtons) As DialogResult
        Try
            Dim result As Integer
#If X7 Then
                 result = CInt(System.Windows.MessageBox.Show(message, caption, CType((CInt(buttons)), System.Windows.MessageBoxButton)))
#ElseIf X8 Then
                 result = CInt(System.Windows.MessageBox.Show(message, caption, CType((CInt(buttons)), System.Windows.MessageBoxButton)))
#Else
            result = CInt(corelApp.FrameWork.ShowMessageBox(message, caption, buttons))
#End If
            Return result
        Catch
            Return DialogResult.Ignor
        End Try
    End Function
End Module





