using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TemplateWizard;

using ProjectHelper;
using System.Text;
using System.Linq;


namespace Wizard001
{

    public class Wizard : WizardBase, IWizard
    {

     
        private EnvDTE.Project project;
       

        public Wizard():base()
        {
           
        }

        public void BeforeOpeningFile(global::EnvDTE.ProjectItem projectItem)
        {
            Console.WriteLine("BeforeOpeningFile");
        }

        public void ProjectFinishedGenerating(global::EnvDTE.Project project)
        {
            this.project = project;
            //System.Windows.Forms.MessageBox.Show("ProjectFinishedGenerating");
            Console.WriteLine("ProjectFinishedGenerating");
        }

        public void ProjectItemFinishedGenerating(global::EnvDTE.ProjectItem projectItem)
        {
            Console.WriteLine("ProjectItemFinishedGenerating");
        }

        public void RunFinished()
        {
            if (!finish)
            {
                this.project.DTE.Solution.Close();
                ClearUp();
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
                    case "2024 Debug":
                        item.Activate();
                        break;
                    case "2022 Debug":
                        item.Activate();
                        break;
                    case "2021 Debug":
                        item.Activate();
                        break;
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

           
        }

        public  void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            BaseRunStarted(replacementsDictionary);
        }
    }
}
