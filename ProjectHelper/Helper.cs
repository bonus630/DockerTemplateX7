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
