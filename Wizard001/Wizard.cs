using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TemplateWizard;
using Microsoft.VisualStudio.Shell.Interop;


namespace Wizard001
{
   
    class Wizard : IWizard
    {
       
        private bool finish = false;
        private string projectDir;
        private EnvDTE.Project project;
        private List<CorelVersionInfo> selectedVersions;

        public void BeforeOpeningFile(global::EnvDTE.ProjectItem projectItem) {
            
        }

        public void ProjectFinishedGenerating(global::EnvDTE.Project project){
            this.project = project;
        }
       
        public void ProjectItemFinishedGenerating(global::EnvDTE.ProjectItem projectItem){
            
        }

        public void RunFinished()
        {
            if(!finish)
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
            List<EnvDTE.SolutionConfiguration> configurations = new List<EnvDTE.SolutionConfiguration>();
            for (int i = 0; i < CorelVersionInfo.MaxVersion - CorelVersionInfo.MinVersion; i++)
            {
                configurations.Add(null);
            }

            foreach (EnvDTE.SolutionConfiguration item in this.project.DTE.Solution.SolutionBuild.SolutionConfigurations)
            {
                if (item.Name == "2019 Debug")
                {
                    configurations[4] = item;
                }
                if (item.Name == "2018 Debug")
                {
                    configurations[3] = item;
                }
                if (item.Name == "2017 Debug")
                {
                    configurations[2] = item;
                }
                if(item.Name == "X7 Debug")
                {
                    configurations[0] = item;
                }
                if(item.Name == "X8 Debug")
                {
                    configurations[1] = item;
                }

            }
            configurations.RemoveAll(r => r == null);
            for (int i = 0; i < configurations.Count; i++)
            {
                if (configurations[i] != null)
                {
                    configurations[i].Activate();
                    configurations[i].DTE.Solution.SolutionBuild.Build(true);
                    break;
                    
                }
                
                   
            }
            
            this.project.Save();
            
            


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
                ConfigureForm form = new ConfigureForm(vsTheme,replacementsDictionary["$type$"]);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bool cancel = true;
                    
                    this.selectedVersions = form.SelectedVersions;
                    for (int i = CorelVersionInfo.MinVersion; i < CorelVersionInfo.MaxVersion; i++)
                    {

                        replacementsDictionary.Add("$corel" + i.ToString() + "$", "0");

                    }
                    for (int i = 0; i <selectedVersions.Count; i++)
                    {
                        CorelVersionInfo corel = selectedVersions[i];
                        if (corel.CorelInstallationNotFound)
                        {
                            System.Windows.Forms.MessageBox.Show(string.Format("{0} not found", corel.CorelFullName));
                            
                           
                        }
                        else
                        {
                            cancel = false;
                            string corelAddonsPath = "";
                            if (corel.Corel64Bit == CorelVersionInfo.CorelIs64Bit.Corel32)
                                corel.CorelAddonsPath(out corelAddonsPath);
                            else
                                corel.CorelAddonsPath64(out corelAddonsPath);
                            replacementsDictionary["$corel" + corel.CorelVersion.ToString() + "$"] = "1";
                            
                            replacementsDictionary.Add("$CorelAddonsPath" + corel.CorelVersion + "$", corelAddonsPath);
                         
                            replacementsDictionary.Add("$CorelProgramPath" + corel.CorelVersion + "$", corel.CorelExePath);
                         
                        }
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
                    throw new WizardCancelledException();
                }
            }
            catch(WizardCancelledException)
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
