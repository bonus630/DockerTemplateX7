using System;
using Corel.Interop.VGCore;

public static class Extensions
{
    /// <summary>
    /// Active optimization features, and start a command group
    /// </summary>
    /// <param name="app"></param>
    /// <param name="commandGroup"></param>
    /// <param name="optimization"></param>
    /// <param name="enableEvents"></param>
    /// <param name="preservSeletion"></param>
	 public static void BeginDraw(this Corel.Interop.VGCore.Application app, bool commandGroup = true, bool optimization = true, bool enableEvents = false, bool preservSeletion = true)
        {
            if (app.ActiveDocument != null)
            {
                if (commandGroup)
                    app.ActiveDocument.BeginCommandGroup();
                if (preservSeletion)
                    app.ActiveDocument.PreserveSelection = preservSeletion;
            }

            app.Optimization = optimization;
            app.EventsEnabled = enableEvents;
        }
    /// <summary>
    /// Desables optimization features, close the command group and reflesh UI
    /// </summary>
    /// <param name="app"></param>
        public static void EndDraw(this Corel.Interop.VGCore.Application app)
        {
            if (app.ActiveDocument != null)
            {
                app.ActiveDocument.EndCommandGroup();
                app.ActiveDocument.PreserveSelection = false;
            }
            app.Optimization = false;
            app.EventsEnabled = true;
            app.Refresh();
        }
}
