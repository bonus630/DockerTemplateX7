using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TemplateWizard;
using Microsoft.VisualStudio.Shell.Interop;
using ProjectHelper;

namespace Wizard2022
{
    public class Wizard : WizardBase, IWizard
    {
        private EnvDTE.Project project;
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
        
            this.project.DTE.Solution.SolutionBuild.Build(true);
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            BaseRunStarted(replacementsDictionary);
        }
    }
}
