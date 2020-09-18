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
        public static readonly Dictionary<string, string> TemplatesIDs = new Dictionary<string,string>()
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
    }
}
