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

            sr.Append("<Project DefaultTargets = \"Compile\" xmlns = \"http://schemas.microsoft.com/developer/msbuild/2003\">\n\r");
            sr.Append("\t<Choose>\n\r");

            for (int i = 0; i < versions.Length; i++)
            {
                sr.AppendFormat("\t\t<When Condition=\"'$(Configuration)' == '{0} Release' or '$(Configuration)' == '{0} Debug'\">\n\r", versions[i].CorelAbreviation);
                sr.Append("\t\t\t<PropertyGroup>\n\r");
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

                sr.AppendFormat("\t\t\t\t<CurrentCorelPath>{0}</CurrentCorelPath>\n\r", corelPath);
                sr.AppendFormat("\t\t\t\t<CurrentCorelAbr>{0}</CurrentCorelAbr>\n\r", versions[i].CorelAbreviation);
                sr.AppendFormat("\t\t\t\t<CurrentCorelDebugConst>{0}</CurrentCorelDebugConst>\n\r", versions[i].CorelDebugConst);
                sr.Append("\t\t\t</PropertyGroup>\n\r");
                sr.Append("\t\t</When>\n\r");
            }
            sr.Append("\t</Choose>\n\r");
            sr.Append("\t<PropertyGroup>\n\r");
            sr.Append("\t\t<VGCoreDLL>Assemblies\\Corel.Interop.VGCore.dll</VGCoreDLL>\n\r");
            sr.Append("\t</PropertyGroup>\n\r");
            sr.Append("\t<Target Name=\"RenameFile\" AfterTargets=\"Build\" Condition=\"'$(TemplateGuid)' == '{d544180d-595b-4d71-b1ab-520a528892cc}' OR '{0AC96025-9E94-4F81-B6FD-C25731EED4A7}' OR '{2005f455-f415-4dfb-a4de-d6d9562b3813}'\">\n\r");
            sr.Append("\t\t<Message Text=\"Rename DLL file to CorelAddon\" />\n\r");
            sr.Append("\t\t<Message Text=\"$(CurrentCorelPath)\" />\n\r");
            sr.Append("\t\t<Exec Condition=\"Exists('$(TargetDir)$(TargetName).CorelAddon')\" Command='del \"$(TargetDir)$(TargetName).CorelAddon\"' />\n\r");
            sr.Append("\t\t<Exec Command='rename \"$(TargetPath)\" \"$(TargetName).CorelAddon\"'\n\r");
            sr.Append("\t</Target>\n\r");
            sr.Append("\t<Target Name=\"CopyFiles\" AfterTargets=\"Build\">\n\r");
            sr.Append("\t\t<Message Text=\"CopyFiles\" />\n\r");
            sr.Append("\t\t<Message Text=\"$(CurrentCorelPath)\" />\n\r");
            sr.Append("\t\t<MakeDir Directories=\"$(CurrentCorelPath)Addons\\$(SolutionName)\" />\n\r");
            sr.Append("\t\t<Exec Condition=\"Exists('$(CurrentCorelPath)Addons\\$(SolutionName)')\" Command='xcopy \"$(ProjectDir)$(OutDir)*.*\" \"$(CurrentCorelPath)Addons\\$(SolutionName)\" /y /d /e /c' />\n\r");
            sr.Append("\t</Target>\n\r");
            sr.Append("</Project>\n\r");
            return sr.ToString();
        }
    }
}
