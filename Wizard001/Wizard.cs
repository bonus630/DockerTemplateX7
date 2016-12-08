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

        public void BeforeOpeningFile(global::EnvDTE.ProjectItem projectItem) {}

        public void ProjectFinishedGenerating(global::EnvDTE.Project project){
            
        }
       
        public void ProjectItemFinishedGenerating(global::EnvDTE.ProjectItem projectItem){}

        public void RunFinished(){
            if(!finish)
            {
                try
                {
                    //this.projectDir = replacementsDictionary["$destinationdirectory$"];
                    if (Directory.Exists(this.projectDir))
                    {
                        Directory.Delete(this.projectDir, true);
                    }
                    throw new WizardCancelledException();
                }
                catch(WizardCancelledException e)
                {
                    
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
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

                    //CorelVersionInfo corel = new CorelVersionInfo(this.corelVersion);
                    CorelVersionInfo corel = form.corelVersionInfo;
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
            catch(WizardCancelledException erro)
            {
                finish = false;
            }
            catch (Exception e)
            {
                finish = false;
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

      
        public bool ShouldAddProjectItem(string filePath)
        {
            return finish;
        }
        
    }
}
