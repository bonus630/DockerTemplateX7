using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TemplateWizard;
using Microsoft.VisualStudio.Shell.Interop;
using ProjectHelper;
using System.Text;
using System.Linq;

namespace Wizard001
{

    class Wizard : IWizard
    {

        private bool finish = false;
        private string projectDir;
        private EnvDTE.Project project;
        private List<CorelVersionInfo> selectedVersions;

        private readonly string CSGuid = "FAE04EC0-301F-11D3-BF4B-00C04F79EFBC";
        private readonly string VBGuid = "F184B08F-C81C-45F6-A57F-5ABD9991F28F";
        private readonly string WPFGuid = "60dc8134-eba5-43b8-bcc9-bb4bc16c2548";


        private Dictionary<string, string> ProjectTypeGuid = new Dictionary<string, string>();



        public Wizard()
        {

            //Guid List
            //https://www.codeproject.com/Reference/720512/List-of-Visual-Studio-Project-Type-GUIDs

            ProjectTypeGuid.Add("$CSGuid$", CSGuid);
            ProjectTypeGuid.Add("$VBGuid$", VBGuid);
            ProjectTypeGuid.Add("$WPFUserControlGuid$", WPFGuid);
        }

        public void BeforeOpeningFile(global::EnvDTE.ProjectItem projectItem)
        {

        }

        public void ProjectFinishedGenerating(global::EnvDTE.Project project)
        {
            this.project = project;
        }

        public void ProjectItemFinishedGenerating(global::EnvDTE.ProjectItem projectItem)
        {

        }

        public void RunFinished()
        {
            if (!finish)
            {
                this.project.DTE.Solution.Close();
                try
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(this.projectDir);
                    if (dirInfo.Parent.Exists)
                    {
                        dirInfo.Parent.Delete(true);
                    }
                }
                catch (IOException e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
                throw new WizardBackoutException();
            }
            this.selectedVersions.OrderBy(r => r.CorelVersion);

            var solutionConfigurations = this.project.DTE.Solution.SolutionBuild.SolutionConfigurations;



            foreach (EnvDTE.SolutionConfiguration item in solutionConfigurations)
            {
                EnvDTE.SolutionConfiguration toDelete = null;
                for (int i = 0; i < this.selectedVersions.Count; i++)
                {
                    if (!item.Name.Contains(this.selectedVersions[i].CorelAbreviation))
                    {


                        toDelete = item;

                    }
                    else
                    {
                        toDelete = null;
                        break;
                    }
                }

                if (toDelete != null)
                {
                    try
                    {
                        Console.WriteLine(toDelete.Name);
                        toDelete.Delete();
                        
                    }
                    catch { }
                }
            }
            foreach (EnvDTE.SolutionConfiguration item in solutionConfigurations)
            {
                switch (item.Name)
                {
                    case "2020 Debug":
                        item.Activate();
                        break;
                    case "2019 Debug":
                        item.Activate();
                        break;
                    case "2018 Debug":
                        item.Activate();
                        break;
                    case "2017 Debug":
                        item.Activate();
                        break;
                    case "X8 Debug":
                        item.Activate();
                        break;
                    case "X7 Debug":
                        item.Activate();
                        break;
                }

            }
            // }
            this.project.DTE.Solution.SolutionBuild.Build(true);

            //foreach (EnvDTE.SolutionConfiguration item in this.project.DTE.Solution.SolutionBuild.SolutionConfigurations)
            //{
            //    if (item.Name == "2020 Debug")
            //    {
            //        configurations[5] = item;
            //        continue;
            //    }
            //    if (item.Name == "2019 Debug")
            //    {
            //        configurations[4] = item;
            //        continue;
            //    }
            //    if (item.Name == "2018 Debug")
            //    {
            //        configurations[3] = item;
            //        continue;
            //    }
            //    if (item.Name == "2017 Debug")
            //    {
            //        configurations[2] = item;
            //        continue;
            //    } 
            //    if(item.Name == "X8 Debug")
            //    {
            //        configurations[1] = item;
            //        continue;
            //    }
            //    if(item.Name == "X7 Debug")
            //    {
            //        configurations[0] = item;
            //        continue;
            //    }


            //}
            //configurations.RemoveAll(r => r == null);


            //for (int i = 0; i < configurations.Count; i++)
            //{
            //    if (configurations[i] != null)
            //    {
            //        configurations[i].Activate();
            //        configurations[i].DTE.Solution.SolutionBuild.Build(true);
            //        break;

            //    }
            //}

            //this.project.Save();
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {

            this.projectDir = replacementsDictionary["$destinationdirectory$"];

            try
            {
                EnvDTE80.DTE2 dte = null;
                VsTheme vsTheme = VsTheme.Unknown;
                try
                {
                    dte = automationObject as EnvDTE80.DTE2;
                }
                catch { }
                if (dte != null)
                {
                    ThemeManager tManager = new ThemeManager(dte);
                    vsTheme = tManager.GetCurrentTheme();
                }
                ConfigureForm form = new ConfigureForm(vsTheme, replacementsDictionary["$type$"]);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bool cancel = true;

                    this.selectedVersions = form.SelectedVersions;
                    DirectoryInfo dir;
                    string targetFolder = "";
                    if (form.GlobalTargets)
                    {
                        replacementsDictionary.Add("$targetvar$", "$([System.Environment]::GetEnvironmentVariable('localappdata'))\\bonus630\\");
                        dir = new DirectoryInfo(System.Environment.GetEnvironmentVariable("localappdata") + "\\bonus630");
                        if (!dir.Exists)
                            dir.Create();
                        targetFolder = dir.FullName;
                    }
                    else
                    {
                        replacementsDictionary.Add("$targetvar$", "$(SolutionDir)");
                        dir = new DirectoryInfo(this.projectDir);
                        if (dir.Exists)
                            targetFolder = dir.Parent.FullName;
                    }

                    if (!string.IsNullOrEmpty(targetFolder))
                    {
                        TargetsCreator targetsCreator = new TargetsCreator();
                        targetsCreator.WriteTargetsFile(targetFolder);
                    }
                    for (int i = CorelVersionInfo.MinVersion; i < CorelVersionInfo.MaxVersion; i++)
                    {

                        replacementsDictionary.Add("$corel" + i.ToString() + "$", "");

                    }
                    foreach (var item in ProjectTypeGuid)
                    {
                        replacementsDictionary.Add(item.Key, item.Value);
                    }
                    for (int i = CorelVersionInfo.MinVersion; i < CorelVersionInfo.MaxVersion; i++)
                    {
                        CorelVersionInfo corel = new CorelVersionInfo(i);
                        //CorelVersionInfo corel = selectedVersions[i];
                        //string corelAddonsPath = "";
                        //if (corel.Corel64Bit == CorelVersionInfo.CorelIs64Bit.Corel32)
                        //    corel.CorelAddonsPath(out corelAddonsPath);
                        //else
                        //    corel.CorelAddonsPath64(out corelAddonsPath);

                        string projectKind = replacementsDictionary["$lang$"];
                        if (projectKind.Equals("CS"))
                            replacementsDictionary["$corel" + corel.CorelVersion.ToString() + "$"] = Helper.buildConfigurationCS(corel);
                        else if (projectKind.Equals("VB"))
                            replacementsDictionary["$corel" + corel.CorelVersion.ToString() + "$"] = Helper.buildConfigurationVB(corel);
                        else
                            throw new WizardCancelledException();
                        if (!corel.CorelInstallationNotFound)
                            cancel = false;
                        //if (corel.CorelInstallationNotFound)
                        //{
                        //    System.Windows.Forms.MessageBox.Show(string.Format("{0} not found", corel.CorelFullName));
                        //}
                        //else
                        //{
                        //    cancel = false;
                        //}
                    }

                    if (cancel)
                    {
                        System.Windows.Forms.MessageBox.Show("Operation is canceled!");

                        throw new WizardCancelledException();

                    }



                    replacementsDictionary.Add("$GuidA$", Guid.NewGuid().ToString());
                    replacementsDictionary.Add("$GuidB$", Guid.NewGuid().ToString());
                    replacementsDictionary.Add("$GuidC$", Guid.NewGuid().ToString());
                    replacementsDictionary.Add("$Caption$", form.DockerCaption);

                    finish = true;

                }
                else
                {
                    finish = false;
                }
            }
            catch (WizardCancelledException)
            {
                finish = false;

            }
            catch (Exception)
            {
                finish = false;

            }
        }
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
       
    }
}
