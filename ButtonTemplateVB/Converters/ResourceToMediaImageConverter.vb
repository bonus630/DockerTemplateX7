Imports System.Globalization
Imports System.Reflection
Imports System.Resources
Imports System.Windows.Interop
Imports System.Windows.Data
Imports System.Windows.Media.Imaging
Imports System
Imports System.Windows

Namespace Converters

    Public Class ResourceToMediaImageConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If
            Dim Image As System.Drawing.Bitmap
            Image = GetBitmap(value.ToString())
            If Image Is Nothing Then
                Return Nothing
            End If

            Dim bitmap = New System.Drawing.Bitmap(Image)
            Dim BitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())

            bitmap.Dispose()

            Return BitmapSource
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        Private Function GetBitmap(ResourceName As String) As System.Drawing.Bitmap
            Dim rm As New ResourceManager("$safeprojectname$.Properties.Resources",
                Assembly.GetExecutingAssembly())
            Try
                Return rm.GetObject(ResourceName)
            Catch ex As ArgumentNullException
                Throw ex
            End Try
        End Function

    End Class
End Namespace



