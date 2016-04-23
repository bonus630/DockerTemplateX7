using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;


namespace Wizard001
{
    class Wizard : IWizard
    {
        private int corelVersion;

        public void BeforeOpeningFile(global::EnvDTE.ProjectItem projectItem) {}

        public void ProjectFinishedGenerating(global::EnvDTE.Project project){}
       
        public void ProjectItemFinishedGenerating(global::EnvDTE.ProjectItem projectItem){}

        public void RunFinished(){}

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            try
            {
                Form1 form = new Form1();
                form.ShowDialog();
                this.corelVersion = form.CorelVersion;
                
                CorelVersionInfo corel = new CorelVersionInfo(this.corelVersion);
                if (corel.CorelInstallationNotFound)
                    System.Windows.Forms.MessageBox.Show(string.Format("{0} not found", corel.CorelFullName));
                else {
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
                }
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show(e.Message); }
        }

      
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
        
    }
}
