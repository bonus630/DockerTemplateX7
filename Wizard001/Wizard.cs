using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TemplateWizard;


namespace Wizard001
{
    class Wizard : IWizard
    {
        private int corelVersion;
        private bool finish = false;
        private string projectDir;
        private EnvDTE.Project project;

        public void BeforeOpeningFile(global::EnvDTE.ProjectItem projectItem) {}

        public void ProjectFinishedGenerating(global::EnvDTE.Project project){
            this.project = project;
        }
       
        public void ProjectItemFinishedGenerating(global::EnvDTE.ProjectItem projectItem){}

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
            
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            this.projectDir = replacementsDictionary["$destinationdirectory$"];
            try
            {
                Form1 form = new Form1();
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.corelVersion = form.CorelVersion;
                    CorelVersionInfo corel = form.CorelVersionSelected;
                    if (corel.CorelInstallationNotFound)
                    {
                        System.Windows.Forms.MessageBox.Show(string.Format("{0} not found", corel.CorelFullName));
                        
                        throw new WizardCancelledException();
                        
                    }
                    else
                    {
                        string corelAddonsPath = "";
                        if (corel.Corel64Bit == CorelVersionInfo.CorelIs64Bit.Corel32)
                            corel.CorelAddonsPath(out corelAddonsPath);
                        else
                            corel.CorelAddonsPath64(out corelAddonsPath);
                        replacementsDictionary.Add("$CorelAddonsPath$", corelAddonsPath);
                        replacementsDictionary.Add("$GuidA$", Guid.NewGuid().ToString());
                        replacementsDictionary.Add("$GuidB$", Guid.NewGuid().ToString());
                        replacementsDictionary.Add("$GuidC$", Guid.NewGuid().ToString());
                        replacementsDictionary.Add("$Caption$", form.DockerCaption);
                        replacementsDictionary.Add("$CorelProgramPath$", corel.CorelExePath);
                        replacementsDictionary.Add("$CorelVersion$", this.corelVersion.ToString());
                        finish = true;
                    }
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
