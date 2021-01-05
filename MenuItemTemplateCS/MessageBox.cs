namespace $safeprojectname$
{
    public static class MessageBox
    {
        public enum DialogButtons
        {
            Ok = 0,
            OkCancel = 1,
            AbortTryIgnor = 2,
            YesNoCancel = 3,
            YesNo = 4,
            TryAgainCancel = 5,
            CancelTryAgainContinue = 6,
            None = 7
        }
        public enum DialogResult
        {
            Ok = 1,
            CancelClose = 2,
            Abort = 3,
            TryAgain = 4,
            Ignor = 5,
            Yes = 6,
            No = 7,
            TryAgainContinue = 10
        }
 
         /// <summary>
         /// Displays a message box that has a message and that returns a result.
         /// </summary>
         /// <param name="message">
         /// A System.String that specifies the text to display.</param>
         /// <returns> A MessageBoxResult value that specifies which message box button
         ///     is clicked by the user.</returns>
         public static DialogResult MsgShow(this Corel.Interop.VGCore.Application corelApp, string message)
        {
            return corelApp.MsgShow(message, "", DialogButtons.Ok);
        }
   
        /// <summary>
        /// Displays a message box that has a message and title bar caption; and that returns
        ///    a result.
        /// </summary>
        /// <param name="message">A System.String that specifies the text to display.</param>
        /// <param name="caption">A System.String that specifies the title bar caption to display.</param>
        /// <returns>  A MessageBoxResult value that specifies which message box button
        ///     is clicked by the user.</returns>
        public static DialogResult MsgShow(this Corel.Interop.VGCore.Application corelApp, string message, string caption)
        {
            return corelApp.MsgShow(message, caption, DialogButtons.Ok);
        }
 
        /// <summary>
        ///   Displays a message box that has a message, title bar caption, button, and icon;
        ///     and that returns a result.
        /// </summary>
        /// <param name="message">A System.String that specifies the text to display.</param>
        /// <param name="caption"A System.String that specifies the title bar caption to display.></param>
        /// <param name="buttons">A MessageBoxButton value that specifies which button or buttons
        ///     to display.</param>
        /// <returns>A MessageBoxResult value that specifies which message box button
        ///    is clicked by the user.</returns>
        public static DialogResult MsgShow(this Corel.Interop.VGCore.Application corelApp, string message, string caption,DialogButtons buttons)
        {
            
            try
            {
#if X7
                int result = (int)System.Windows.MessageBox.Show(message,caption,(System.Windows.MessageBoxButton)((int)buttons));

#elif X8
                int result = (int)System.Windows.MessageBox.Show(message,caption,(System.Windows.MessageBoxButton)((int)buttons));
#else
               int result = corelApp.FrameWork.ShowMessageBox(message, caption,(int)buttons);
               
#endif
                return (DialogResult)result;
            }
            catch { return DialogResult.Ignor; }
        }
       
       
    }
}
