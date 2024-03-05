using System;
using System.Collections.Generic;
using System.IO;
using ProjectHelper;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace ProjectHelper
{

    public class WizardBase 
    {

        protected bool finish = false;
        protected string projectDir;
        
        protected List<CorelVersionInfo> selectedVersions;
        
        protected readonly string CSGuid = "FAE04EC0-301F-11D3-BF4B-00C04F79EFBC";
        protected readonly string VBGuid = "F184B08F-C81C-45F6-A57F-5ABD9991F28F";
        protected readonly string WPFGuid = "60dc8134-eba5-43b8-bcc9-bb4bc16c2548";
       
        protected Dictionary<string, string> ProjectTypeGuid = new Dictionary<string, string>();



        public WizardBase()
        {

            //Guid List
            //https://www.codeproject.com/Reference/720512/List-of-Visual-Studio-Project-Type-GUIDs

            ProjectTypeGuid.Add("$CSGuid$", CSGuid);
            ProjectTypeGuid.Add("$VBGuid$", VBGuid);
            ProjectTypeGuid.Add("$WPFUserControlGuid$", WPFGuid);
        }

        public void BaseRunStarted(Dictionary<string, string> replacementsDictionary)
        {
            this.projectDir = replacementsDictionary["$destinationdirectory$"];

            try
            {

                ConfigureForm form = new ConfigureForm(replacementsDictionary["$type$"]);
                //ConfigureUI form = new ConfigureUI(replacementsDictionary["$type$"]);
                if (form.ShowDialog()==DialogResult.OK)
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


                        string projectKind = replacementsDictionary["$lang$"];
                        if (projectKind.Equals("cs"))
                            replacementsDictionary["$corel" + corel.CorelVersion.ToString() + "$"] = Helper.buildConfigurationCS(corel);
                        else if (projectKind.Equals("vb"))
                            replacementsDictionary["$corel" + corel.CorelVersion.ToString() + "$"] = Helper.buildConfigurationVB(corel);
                        else
                            throw new Exception();
                        if (!corel.CorelInstallationNotFound)
                            cancel = false;

                    }

                    if (cancel)
                    {
                        System.Windows.Forms.MessageBox.Show("Operation is canceled!");

                        throw new Exception("Operation is canceled!");

                    }



                    replacementsDictionary.Add("$GuidA$", Guid.NewGuid().ToString());
                    replacementsDictionary.Add("$GuidB$", Guid.NewGuid().ToString());
                    replacementsDictionary.Add("$GuidC$", Guid.NewGuid().ToString());
                    replacementsDictionary.Add("$GuidD$", Guid.NewGuid().ToString());
                    replacementsDictionary.Add("$GuidE$", Guid.NewGuid().ToString());
                    replacementsDictionary.Add("$Caption$", form.DockerCaption);

                    finish = true;

                }
                else
                {
                    finish = false;
                    throw new Exception();
                }
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
        public void ClearUp()
        {
            
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
            throw new Exception();
        }

     

     
    }
}
