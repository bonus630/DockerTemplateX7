using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelper
{
    public static class Helper
    {
        public static readonly Dictionary<string, string> TemplatesIDs = new Dictionary<string, string>()
        {
            {"ControlTemplateGuidCS", "d544180d-595b-4d71-b1ab-520a528892cc" },
            {"ControlTemplateGuidVB","CF565DDA-4446-48A5-947E-DD67D439F0C0" } ,
            {"DockerTemplateGuidCS","8AAFD9D4-608E-49AA-8035-EEB1F6F86213" },
            {"DockerTemplateGuidVB","a945a49f-2e0b-400e-8062-c11b2cff0aa6" },
            {"ToolTemplateGuidCS","678f71b7-abf7-4164-8850-df07f94c2fd9" },
            {"ToolTemplateGuidVB","2005f455-f415-4dfb-a4de-d6d9562b3813<" }
        };

        public static bool CheckSolutionIsCDRAddon(string projectFolderPath)
        {
            FileInfo[] files = (new DirectoryInfo(projectFolderPath)).GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name == "CorelDrw.addon")
                    return true;
            }
            return false;
        }

        public static string buildConfigurationCS(CorelVersionInfo corelVersion)
        {
            StringBuilder sr = new StringBuilder();

            sr.AppendFormat("<PropertyGroup Condition=\"'$(Configuration)|$(Platform)' == '{0} Debug|AnyCPU'\">", corelVersion.CorelAbreviation);
            sr.Append("<DebugSymbols>true</DebugSymbols>");
            sr.Append("<DebugType>full</DebugType>");
            sr.Append("<Optimize>false</Optimize>");
            sr.Append("<OutputPath>bin\\Debug\\</OutputPath>");
            sr.AppendFormat("<DefineConstants>DEBUG;TRACE;{0}</DefineConstants>", corelVersion.CorelDebugConst);
            sr.Append("<ErrorReport>prompt</ErrorReport>");
            sr.Append("<WarningLevel>4</WarningLevel>");
            sr.Append("</PropertyGroup>");
            sr.AppendFormat("<PropertyGroup Condition=\"'$(Configuration)|$(Platform)' == '{0} Release|AnyCPU'\">", corelVersion.CorelAbreviation);
            sr.Append("<DebugType>none</DebugType>");
            sr.Append("<Optimize>true</Optimize>");
            sr.Append("<OutputPath>bin\\Release\\$(CurrentCorelAbs)\\</OutputPath>");
            sr.Append("<OutDir>bin\\Release\\$(CurrentCorelAbs)\\$(SolutionName)</OutDir>");
            sr.AppendFormat("<DefineConstants>TRACE;{0}</DefineConstants>", corelVersion.CorelDebugConst);
            sr.Append("<ErrorReport>prompt</ErrorReport>");
            sr.Append("<WarningLevel>4</WarningLevel>");
            sr.Append("</PropertyGroup>");

            return sr.ToString();
        }
        public static string buildConfigurationVB(CorelVersionInfo corelVersion)
        {
            StringBuilder sr = new StringBuilder();

            sr.AppendFormat("<PropertyGroup Condition=\"'$(Configuration)|$(Platform)' == '{0} Debug|AnyCPU'\">", corelVersion.CorelAbreviation);
            sr.Append("<DebugSymbols>true</DebugSymbols>");
            sr.Append("<DebugType>full</DebugType>");
            sr.Append("<DefineDebug>true</DefineDebug>");
            sr.Append("<DefineTrace>true</DefineTrace>");
            sr.Append("<OutputPath>bin\\Debug\\</OutputPath>");
            sr.Append("<OutDir>bin\\Debug\\</OutDir>");
            sr.Append("<DocumentationFile></DocumentationFile>");
            sr.Append("<NoWarn>42016,41999,42017,42018,42019,42032,42036,42020,42021,42022</NoWarn>");
            sr.AppendFormat("<DefineConstants>DEBUG,{0}</DefineConstants>", corelVersion.CorelDebugConst);
            sr.Append("</PropertyGroup>");
            sr.AppendFormat("<PropertyGroup Condition=\"'$(Configuration)|$(Platform)' == '{0} Release|AnyCPU'\">", corelVersion.CorelAbreviation);
            sr.Append("<DebugType>none</DebugType>");
            sr.Append("<DefineDebug>false</DefineDebug>");
            sr.Append("<DefineTrace>true</DefineTrace>");
            sr.Append("<Optimize>true</Optimize>");
            sr.Append("<OutputPath>bin\\Release\\$(CurrentCorelAbs)\\</OutputPath>");
            sr.Append("<OutDir>bin\\Release\\$(CurrentCorelAbs)\\$(SolutionName)</OutDir>");
            sr.Append("<DocumentationFile></DocumentationFile>");
            sr.Append("<NoWarn>42016,41999,42017,42018,42019,42032,42036,42020,42021,42022</NoWarn>");
            sr.AppendFormat("<DefineConstants>{0}</DefineConstants>", corelVersion.CorelDebugConst);
            sr.Append("</PropertyGroup>");

            return sr.ToString();
        }
    }
}
