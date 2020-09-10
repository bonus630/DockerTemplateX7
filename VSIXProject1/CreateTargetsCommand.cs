using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using ProjectHelper;
using Task = System.Threading.Tasks.Task;

namespace VSIXProject1
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CreateTargetsCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("1d48d59a-b75b-4d3e-a9e6-d64cd76391c4");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTargetsCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private CreateTargetsCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandID);
            menuItem.BeforeQueryStatus += MenuItem_BeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        private void MenuItem_BeforeQueryStatus(object sender, EventArgs e)
        {
            OleMenuCommand c = sender as OleMenuCommand;
            string projectFolderPath = "";
            Task t = Task.Run(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                EnvDTE.DTE dte;
                dte = (EnvDTE.DTE)Package.GetGlobalService(typeof(EnvDTE.DTE));


                EnvDTE.Project project;
                project = dte.Solution.Projects.Item(1);

                string uni = project.FullName;

                FileInfo file = new FileInfo(uni);
                projectFolderPath = file.Directory.FullName;
                c.Visible = Helper.CheckSolutionIsCDRAddon(projectFolderPath);
            });
            
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CreateTargetsCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in CreateTargetsCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new CreateTargetsCommand(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            string title = "CreateTargetsCommand";

            // Show a message box to prove we were here
            //VsShellUtilities.ShowMessageBox(
            //    this.package,
            //    message,
            //    title,
            //    OLEMSGICON.OLEMSGICON_INFO,
            //    OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            EnvDTE.DTE dte;
            dte = (EnvDTE.DTE)Package.GetGlobalService(typeof(EnvDTE.DTE));

            string solutionFolder = "";
            EnvDTE.Project project;
            project = dte.Solution.Projects.Item(1);

            string uni = project.FullName;

            FileInfo file = new FileInfo(uni);
            solutionFolder = file.Directory.Parent.FullName;

            if (!Helper.CheckSolutionIsCDRAddon(file.Directory.FullName))
                return;


            try
            {


                if (Directory.Exists(solutionFolder))
                {
                    string targetsFile = Path.Combine(solutionFolder, "bonus630.CDRCommon.targets");
                    File.Delete(targetsFile);
                    ProjectHelper.TargetsCreator targetsCreator = new ProjectHelper.TargetsCreator();
                    if (targetsCreator.WriteTargetsFile(solutionFolder))
                        dte.StatusBar.Text = "CorelDraw Paths is updated!";
                }
            }
            catch (Exception erro)
            {
                dte.StatusBar.Text = erro.Message;
            }
        }
    }
}
