using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectHelper
{
    /// <summary>
    /// Interaction logic for ConfigureUI.xaml
    /// </summary>
    public partial class ConfigureUI : Window
    {
        private string virtualFolder = "";

        Regex regex;

        public int CorelVersion { get; private set; }
        private string dockerCaption = "Enter Caption";
        public string DockerCaption { get { return dockerCaption; }  set { dockerCaption = value; } }

        public bool HasCaption { get; private set; }

        private bool globalTargets = true;
        public bool GlobalTargets { get { return globalTargets; } protected set { globalTargets = value; } }

        private List<CorelVersionInfo> installedVersions = new List<CorelVersionInfo>();
        private List<CorelVersionInfo> selectedVersions;

        public List<CorelVersionInfo> SelectedVersions
        {
            get { return selectedVersions; }
            set { selectedVersions = value; }
        }
        private List<VersionsModel> versions = new List<VersionsModel>();
        public List<VersionsModel> Versions { get { return versions; } set { versions = value; } }

        private string replacementsDictionaryType = "Docker";
       

        public ConfigureUI()
        {
            InitializeComponent();
        }
        public ConfigureUI(string replacementsDictionaryType)
        {
            InitializeComponent();
            
            this.replacementsDictionaryType = replacementsDictionaryType;
            InitializeVersions();
        }
        private void InitializeVersions()
        {
            CorelVersionInfo temp;
            string version = "";
            for (int i = CorelVersionInfo.MinVersion; i < CorelVersionInfo.MaxVersion; i++)
            {
                temp = new CorelVersionInfo(i);
                installedVersions.Add(temp);
                versions.Add(new VersionsModel(i, temp.Corel64FullName, !temp.CorelInstallationNotFound));
                if (i != CorelVersionInfo.MinVersion && i != CorelVersionInfo.MaxVersion)
                {
                    version += "|";
                }
                version += CorelVersionInfo.GetCorelAbreviation(i);
            }
            regex = new Regex(string.Format(@".+\\Corel\\CorelDRAW Graphics Suite (?<corelAbb>(?:{0}))\\Programs(?:64|)$", version));
            //ChangeTheme();
            ChangeType();
            this.DataContext = this;
        }

        private void btn_done_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ChangeType()
        {
            if (replacementsDictionaryType == "Tool")
            {
                HasCaption = false;
                this.Title = "Configure Custom Tool Project";
            }

        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            CorelVersionInfo temp = installedVersions.Find(r => r.CorelVersion == (int)ck.Tag);
            if ((bool)ck.IsChecked)
            {
                if (temp.CorelInstallationNotFound)
                {
                    if (!string.IsNullOrEmpty(virtualFolder))
                    {
                        if (regex.IsMatch(virtualFolder))
                        {
                            string toReplace = regex.Match(virtualFolder).Result("${corelAbb}");
                            virtualFolder = virtualFolder.Replace(toReplace, temp.CorelAbreviation);
                        }
                        else
                            virtualFolder = "";
                    }
                    if (!temp.recoverPathManually(temp.CorelVersion, virtualFolder))
                        ck.IsChecked = false;

                }
                if (!temp.CorelInstallationNotFound)
                {
                    this.selectedVersions.Add(temp);
                    if (temp.Corel64Bit == CorelVersionInfo.CorelIs64Bit.Corel32)
                        virtualFolder = temp.CorelExePath;
                    else
                        virtualFolder = temp.CorelExePath;


                }
            }
            if (!(bool)ck.IsChecked && this.selectedVersions.Count > 0)
                this.selectedVersions.Remove(installedVersions.Find(r => r.CorelVersion == (int)ck.Tag));
            if (this.selectedVersions.Count > 0)
                btn_done.IsEnabled = true;
            else
                btn_done.IsEnabled = false;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            this.GlobalTargets = (bool)(sender as RadioButton).IsChecked;
        }
    }
}
