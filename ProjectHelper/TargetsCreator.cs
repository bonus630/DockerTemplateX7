using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectHelper;

namespace ProjectHelper
{
    public class TargetsCreator
    {
        CorelVersionInfo[] versions;
        readonly string targetsName = "bonus630.CDRCommon.targets";
        public TargetsCreator()
        {
            versions = new CorelVersionInfo[CorelVersionInfo.MaxVersion - CorelVersionInfo.MinVersion];
            for (int i = 0; i < versions.Length; i++)
            {
                versions[i] = new CorelVersionInfo(i + CorelVersionInfo.MinVersion);
            }
        }
        public bool WriteTargetsFile(string solutionPath)
        {
            try
            {
                string path = Path.Combine(solutionPath, targetsName);
                if (File.Exists(path))
                    File.Delete(path);
                File.AppendAllText(path, buildProjectTargetString());
                return true;
            }
            catch
            {
                return false;
            }
        }
        private string buildProjectTargetString()
        {
            StringBuilder sr = new StringBuilder();

            sr.Append("<Project DefaultTargets = \"Compile\" xmlns = \"http://schemas.microsoft.com/developer/msbuild/2003\">");
            sr.Append("<Choose>");

            for (int i = 0; i < versions.Length; i++)
            {
                sr.AppendFormat("<When Condition=\"'$(Configuration)' == '{0} Release' or '$(Configuration)' == '{0} Debug'\">", versions[i].CorelAbreviation);
                sr.Append("<PropertyGroup>");
                string corelPath = "";

                if (!versions[i].CorelInstallationNotFound)
                {
                    if (versions[i].Corel64Bit == CorelVersionInfo.CorelIs64Bit.Corel32)
                    {
                        versions[i].CorelAddonsPath(out corelPath);
                    }
                    else
                        versions[i].CorelAddonsPath64(out corelPath);
                    DirectoryInfo dir = new DirectoryInfo(corelPath);
                    if (dir.Exists)
                    {
                        corelPath = dir.Parent.FullName + "\\";
                    }
                    else
                    {
                        corelPath = "";
                    }
                }

                sr.AppendFormat("<CurrentCorelPath>{0}</CurrentCorelPath>", corelPath);
                sr.AppendFormat("<CurrentCorelAbr>{0}</CurrentCorelAbr>", versions[i].CorelAbreviation);
                sr.AppendFormat("<CurrentCorelDebugConst>{0}</CurrentCorelDebugConst>", versions[i].CorelDebugConst);
                sr.Append("</PropertyGroup>");
                sr.Append("</When>");
            }
            sr.Append("</Choose>");
            sr.Append("<PropertyGroup>");
            sr.Append("<VGCoreDLL>Assemblies\\Corel.Interop.VGCore.dll</VGCoreDLL>");
            sr.Append("</PropertyGroup> ");
            sr.Append("<Target Name=\"CopyFiles\" AfterTargets=\"Build\">");
            sr.Append("<Message Text=\"CopyFiles\" />");
            sr.Append("<Message Text=\"$(CurrentCorelPath)\" />");
            sr.Append("<MakeDir Directories=\"$(CurrentCorelPath)Addons\\$(SolutionName)\" />");
            sr.Append("<Exec Condition=\"Exists('$(CurrentCorelPath)Addons\\$(SolutionName)')\" Command='xcopy \"$(ProjectDir)$(OutDir)*.*\" \"$(CurrentCorelPath)Addons\\$(SolutionName)\" /y /d /e /c' />");
            sr.Append("</Target>");
            sr.Append("</Project>");
            return sr.ToString();
        }
    }
}
