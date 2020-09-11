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
    internal sealed class OpenAddonFolder
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 256;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("966034e1-2b7b-4876-b0ae-b7ded2abed89");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenAddonFolder"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private OpenAddonFolder(AsyncPackage package, OleMenuCommandService commandService)
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
        public static OpenAddonFolder Instance
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
            // Switch to the main thread - the call to AddCommand in OpenAddonFolder's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new OpenAddonFolder(package, commandService);
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
            EnvDTE.DTE dte;
            dte = (EnvDTE.DTE)Package.GetGlobalService(typeof(EnvDTE.DTE));

            string solutionFolder = "";
            EnvDTE.Project project;
            project = dte.Solution.Projects.Item(1);
            string configuration = project.ConfigurationManager.ActiveConfiguration.ConfigurationName;

            CorelVersionInfo corelVersionInfo =  null;

            if(configuration.Contains("Release"))
            {
                corelVersionInfo = new CorelVersionInfo(configuration.Replace(" Release", ""));
            }
            if (configuration.Contains("Debug"))
            {
                corelVersionInfo = new CorelVersionInfo(configuration.Replace(" Debug", ""));
            }
            if (corelVersionInfo == null)
                return;

            if(!corelVersionInfo.CorelInstallationNotFound)
            {
                string path = "";
                if (corelVersionInfo.Corel64Bit == CorelVersionInfo.CorelIs64Bit.Corel32)
                    corelVersionInfo.CorelAddonsPath(out path);
                else
                    corelVersionInfo.CorelAddonsPath64(out path);
                if (!string.IsNullOrEmpty(path))
                    System.Diagnostics.Process.Start(path);
            }


        }
    }
}
