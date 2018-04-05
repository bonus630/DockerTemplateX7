using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
namespace Wizard001
{
    public class CorelVersionInfo
    {
      
        public enum CorelIs64Bit
        {
            Corel32,
            Corel64,
            CorelBoth

        }
            private bool corelInstallationNotFound = true;

            public string CorelAbreviation { get { return this.corelAbb[this.CorelVersion - 10]; } }
            public string CorelFullName { get { return string.Format("{0} {1}", corelName, this.corelAbb[this.CorelVersion - 10]); } }
            public string CorelFolderName { get { return this.corelFolderList[this.CorelVersion - 10]; } }
            public string Corel64FullName { get { return string.Format("{0} {1} 64bit", corelName, this.corelAbb[this.CorelVersion - 10]); } }
            private string corelGMSPath;
            //private string corelAddonsPath;
            //private string corelGMSPath64;
            //private string corelAddonsPath64;
            public bool InList { get; private set; }
            private bool defaultInstallationPath = false;
            public bool CorelInstallationNotFound { get { return this.corelInstallationNotFound; } protected set { } }
            public string CorelInstallationPath { get; private set; }
            public string CorelExePath { get; private set; }
            public string CorelInstallationPath64 { get; private set; }
            public CorelIs64Bit Corel64Bit { get; set; }
            public int CorelVersion { get; private set; }
            private string[] corelAbb = new string[] { "10", "11", "12", "X3", "X4", "X5", "X6", "X7", "X8", "2017","2018" };
            private string[] corelFolderList = new string[] { "Graphics10", "Corel Graphics 11", "Corel Graphics 12",
                "CorelDRAW Graphics Suite 13", "CorelDRAW Graphics Suite X4", "CorelDRAW Graphics Suite X5",
                "CorelDRAW Graphics Suite X6", "CorelDRAW Graphics Suite X7", "CorelDRAW Graphics Suite X8",
                "CorelDRAW Graphics Suite 2017","CorelDRAW Graphics Suite 2018" };

            private const string corelName = "CorelDraw Graphics Suite";

            //Corel 13 folder - CorelDRAW Graphics Suite 13
            //Corel 10 folder -  Graphics10

            public CorelVersionInfo(int corelVersion, CorelIs64Bit corel64Bit)
            {

                this.CorelVersion = corelVersion;
                this.Corel64Bit = corel64Bit;
            }
            public CorelVersionInfo(int corelVersion)
            {
                this.CorelVersion = corelVersion;
                this.InList = true;
                searchCorelVersion(corelVersion);

            }
            public CorelVersionInfo(string corelAbb)
            {
                this.InList = false;
                for (int i = 0; i < this.corelAbb.Length; i++)
                {
                    if (this.corelAbb[i] == corelAbb.ToUpper())
                    {
                        this.InList = true;
                        this.CorelVersion = i + 10;
                        searchCorelVersion(this.CorelVersion);
                        break;
                    }
                }


            }
            public void setInstallationPath(string corelExePath)
            {
                //Wrong method
                DirectoryInfo dir = new DirectoryInfo(corelExePath.ToLower());
                this.corelInstallationNotFound = false;
                this.CorelExePath = corelExePath;
                if (dir.FullName.Contains("programs64"))
                {
                    
                    this.CorelInstallationPath64 = new DirectoryInfo(corelExePath).Parent.FullName;
                    this.Corel64Bit = CorelIs64Bit.Corel64;
                }
                else
                {
                    this.CorelInstallationPath = new DirectoryInfo(corelExePath).Parent.FullName;
                    this.Corel64Bit = CorelIs64Bit.Corel32;
                }
            }
            private void searchCorelVersion(int corelVersion)
            {

                bool search = false;
                if (!search)
                    search = recoverPathFromRegistryRoot(corelVersion);
                if (!search)
                    search = checkDefaultInstalationPath(corelVersion);
                if (!search)
                    search = recoverPathFromRegistryProgramID(corelVersion);
                if (!search)
                    search = recoverGSMPathFromAssembly(corelVersion);
               // if (!search)
                //    search = recoverPathManually(corelVersion);

                this.corelInstallationNotFound = !search;
              

            }
         
            private bool recoverPathSearchExe(int corelVersion)
            {
                bool result = false;
                DriveInfo[] drives = DriveInfo.GetDrives();
                string systemDir = System.Environment.SystemDirectory;
                //string seachDir = "Corel";
                DirectoryInfo dir = new DirectoryInfo(systemDir);
                IEnumerable<DirectoryInfo> testes = dir.EnumerateDirectories(systemDir, SearchOption.AllDirectories);
                if (this.Corel64Bit.Equals(CorelIs64Bit.Corel32))
                {


                }
                if (this.Corel64Bit.Equals(CorelIs64Bit.Corel64))
                {

                }
                // Parallel.ForEach() 
                return result;
            }
            public bool recoverPathManually(int corelVersion)
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Filter = "|CorelDRW.exe";
                fd.Title = "Please find EXE file of " + Corel64FullName;
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(fd.FileName);
                    string path = fi.DirectoryName;
                    if (path.Contains("Programs64"))
                    {
                        this.setInstallationPath(path);
                        this.Corel64Bit = CorelIs64Bit.Corel64;
                    }
                    else
                    {
                        this.setInstallationPath(path);
                        this.Corel64Bit = CorelIs64Bit.Corel32;
                    }
                    return true;
                }
                else
                    return false;
            }
            private bool validInstallationPath(string path)
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                return dir.Exists;
            }
            //public string CorelGMSPath(bool _64bit)
            //{
            //    if (_64bit)
            //        return String.Format("{0}\\Draw\\GMS", this.CorelInstallationPath64);
            //    else
            //        return String.Format("{0}\\Draw\\GMS", this.CorelInstallationPath);
            //}
            //public string CorelAddonsPath(bool _64bit)
            //{
            //    if (_64bit)
            //        return String.Format("{0}\\Programs64\\Addons", this.CorelInstallationPath64);
            //    else
            //        return String.Format("{0}\\Programs\\Addons", this.CorelInstallationPath);
            //}
            /// <summary>
            /// Recovery GMS path in 64bit version
            /// </summary>
            /// <param name="corelGmsPath">Container path</param>
            /// <returns>Return false if not exists</returns>
            public bool CorelGMSPath(out string corelGmsPath)
            {
                corelGmsPath = "";

                if (!string.IsNullOrEmpty(this.CorelInstallationPath))
                    corelGmsPath = string.Format("{0}{1}\\Draw\\GMS\\", this.CorelInstallationPath, this.defaultInstallationPath ? "\\" + this.CorelFolderName : "");
                if (string.IsNullOrEmpty(corelGmsPath))
                    return false;
                else
                    return true;

            }
            /// <summary>
            /// Recovery Addons path in 32bit version
            /// </summary>
            /// <param name="corelAddonsPath">Container path</param>
            /// <returns>Return false if not exists</returns>
            public bool CorelAddonsPath(out string corelAddonsPath)
            {
                corelAddonsPath = "";

                if (!string.IsNullOrEmpty(this.CorelInstallationPath))
                    corelAddonsPath = string.Format("{0}\\programs\\addons\\", this.CorelInstallationPath);
                if (string.IsNullOrEmpty(corelAddonsPath))
                    return false;
                else
                    return true;
            }
            /// <summary>
            /// Recovery GMS path in 64bit version
            /// </summary>
            /// <param name="corelGmsPath64">Container path</param>
            /// <returns>Return false if not exists</returns>
            public bool CorelGMSPath64(out string corelGmsPath64)
                {
                corelGmsPath64 = "";
                if (!string.IsNullOrEmpty(this.CorelInstallationPath64))
                    corelGmsPath64 = string.Format("{0}{1}\\Draw\\GMS\\", this.CorelInstallationPath64, this.defaultInstallationPath ? "\\" + this.CorelFolderName : "");
                if (string.IsNullOrEmpty(corelGmsPath64))
                    return false;
                else
                    return true;

            }
            /// <summary>
            /// Recovery Addons path in 64bit version
            /// </summary>
            /// <param name="corelAddonsPath64">Container path</param>
            /// <returns>Return false if not exists</returns>
            public bool CorelAddonsPath64(out string corelAddonsPath64)
            {
                corelAddonsPath64 = "";
                if (!string.IsNullOrEmpty(this.CorelInstallationPath64))
                    corelAddonsPath64 = string.Format("{0}\\programs64\\addons\\", this.CorelInstallationPath64);
                if (string.IsNullOrEmpty(corelAddonsPath64))
                    return false;
                else
                    return true;
            }
            private bool recoverGSMPathFromAssembly(int version)
            {
                //Return programs path
                try
                {
                    Type type2 = Type.GetTypeFromProgID("CorelDRAW.Application." + version, true);
                    dynamic vc = (dynamic)Activator.CreateInstance(type2);
                    this.corelGMSPath = ((string)vc.GMSManager.UserGMSPath);
                    string path = (string)vc.ProgramPath;
                    type2 = null; vc = null;

                    if (path.Contains("Programs64"))
                    {
                        this.setInstallationPath(path);
                        this.Corel64Bit = CorelIs64Bit.Corel64;
                    }
                    else
                    {
                        this.setInstallationPath(path);
                        this.Corel64Bit = CorelIs64Bit.Corel32;
                    }
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            private bool checkDefaultInstalationPath(int version)
            {
                //return exe path
                if (Environment.Is64BitOperatingSystem)
                {
                    DirectoryInfo dir64 = new DirectoryInfo(String.Format("{0}\\Corel\\{1}\\programs64", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), this.CorelFolderName));
                    if (dir64.Exists)
                    {

                        this.Corel64Bit = CorelIs64Bit.Corel64;
                        this.defaultInstallationPath = true;
                        this.setInstallationPath(dir64.FullName);
                    }
                    DirectoryInfo dir = new DirectoryInfo(String.Format("{0}\\Corel\\{1}\\programs", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), this.CorelFolderName));
                    if (dir.Exists)
                    {

                        this.Corel64Bit = CorelIs64Bit.Corel32;
                        this.defaultInstallationPath = true;
                        this.setInstallationPath(dir.FullName);
                    }
                    if (dir64.Exists && dir.Exists)
                        this.Corel64Bit = CorelIs64Bit.CorelBoth;
                    if (dir.Exists || dir64.Exists)
                        return true;

                }
                else
                {
                    DirectoryInfo dir = new DirectoryInfo(String.Format("{0}\\Corel\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), this.CorelFolderName));
                    if (dir.Exists)
                    {
                        this.setInstallationPath(dir.FullName);
                        this.Corel64Bit = CorelIs64Bit.Corel32;
                        this.defaultInstallationPath = true;
                        return true;
                    }

                }
                return false;
            }

            private bool recoverPathFromRegistryRoot(int version)
            {
                //Return programs path
                try
                {
                    Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(String.Format("CorelDRAW.Graphic.{0}\\shell\\Open\\command", version));
                    string path = (string)key.GetValue("");
                    path = path.Substring(1).ToLower();
                    //Corel 10 use %1
                    path = path.Replace("coreldrw.exe\" -dde", "");
                    path = path.Replace("coreldrw.exe\" %1", "");
                    key.Close();
                    if (path.Contains("programs64"))
                    {
                        this.setInstallationPath(new DirectoryInfo(path).FullName);
                        this.Corel64Bit = CorelIs64Bit.Corel64;
                    }
                    else
                    {
                        this.setInstallationPath(new DirectoryInfo(path).FullName);
                        this.Corel64Bit = CorelIs64Bit.Corel32;
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            private bool recoverPathFromRegistryApplicationPreference(int version)
            {
                //this it's not safe
                try
                {
                    Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(String.Format(
                        "Software\\Corel\\CorelDRAW\\{0}.0\\Draw\\Application Preferences\\VBA", version));
                    if (key == null)
                        key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(String.Format(
                                            "Software\\Corel\\CorelDRAW\\{0}.0\\CorelDraw\\Application Preferences\\VBA", version));
                    string[] keysName = key.GetSubKeyNames();

                    for (int i = 0; i < keysName.Length; i++)
                    {
                        Microsoft.Win32.RegistryKey tempKey = key.OpenSubKey(keysName[i] + "\\Draw\\Application Preferences\\VBA");
                        if (tempKey == null)
                            tempKey = key.OpenSubKey(keysName[i] + "\\CorelDRAW\\Application Preferences\\VBA");
                        int index = Int16.Parse(keysName[i].Substring(0, keysName[i].IndexOf('.')));
                        string path = (string)tempKey.GetValue("LastGMSFolder");
                        this.corelGMSPath = path;

                        tempKey.Close();
                        return true;
                    }

                    return false;
                }
                catch
                {
                    return false;
                }
            }
            private bool recoverPathFromRegistryProgramID(int version)
            {
                //Return programs path
                Microsoft.Win32.RegistryKey key;
                try
                {
                    key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(String.Format("SOFTWARE\\Corel\\CorelDRAW\\{0}.0", version));
                    string path = (string)key.GetValue("ProgramsDir");

                    key.Close();
                    if (path.Contains("Programs64"))
                    {
                        this.setInstallationPath(new DirectoryInfo(path).FullName);
                        this.Corel64Bit = CorelIs64Bit.Corel64;
                    }
                    else
                    {
                        this.setInstallationPath(new DirectoryInfo(path).FullName);
                        this.Corel64Bit = CorelIs64Bit.Corel32;
                    }

                    return true;

                }
                catch
                {
                    return false;
                }

            }

        }
    }


