using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio;
using ProjectHelper;
using System.Text.RegularExpressions;

namespace Wizard001
{
    public partial class ConfigureForm : Form
    {
        private string virtualFolder = "";
        Regex regex ;

        public int CorelVersion { get; private set; }
        public string DockerCaption { get; private set; }

        private bool globalTargets = true;
        public bool GlobalTargets { get { return globalTargets; } protected set { globalTargets = value; } }

        private List<CorelVersionInfo> installedVersions = new List<CorelVersionInfo>();
        private List<CorelVersionInfo> selectedVersions = new List<CorelVersionInfo>();
        public List<CorelVersionInfo> SelectedVersions { get { return this.selectedVersions; } }
        private VsTheme vsTheme = VsTheme.Unknown;
        private string type = "Docker";
        private Color CkVersionColor { get; set; }
        public ConfigureForm()
        {
            InitializeForm();
            
        }
      
        public ConfigureForm(VsTheme vsTheme,string type)
        {
            this.vsTheme = vsTheme;
            this.type = type;
            InitializeForm();
        }
        private void InitializeForm()
        {
            InitializeComponent();
            
            txt_dockerCaption.Focus();
            DockerCaption = txt_dockerCaption.Text;
            btn_cancel.DialogResult = DialogResult.Cancel;
            btn_done.DialogResult = DialogResult.OK;
            CorelVersionInfo temp;
            string version = "";
            for (int i = CorelVersionInfo.MinVersion; i < CorelVersionInfo.MaxVersion; i++)
            {
                temp = new CorelVersionInfo(i);
                installedVersions.Add(temp);
                AddCheckBox(temp.Corel64FullName, i, !temp.CorelInstallationNotFound);
                if(i!= CorelVersionInfo.MinVersion && i != CorelVersionInfo.MaxVersion)
                {
                    version += "|";
                }
                version += CorelVersionInfo.GetCorelAbreviation(i);
            }
            regex = new Regex(string.Format(@".+\\Corel\\CorelDRAW Graphics Suite (?<corelAbb>(?:{0}))\\Programs(?:64|)$", version));
            ChangeTheme();
            ChangeType();
        }
        private void ChangeTheme()
        { 
            switch(vsTheme)
            {
                case VsTheme.Dark:
                    this.BackColor = Color.FromArgb(45, 45, 48);
                    this.CkVersionColor = Color.White;
                    break;
                default:
                    this.BackColor = Color.White;
                    this.CkVersionColor = Color.FromArgb(165,154,151);
                break;
            }
            foreach (CheckBox item in this.flowLayoutPanel_Versions.Controls)
            {
                item.ForeColor = CkVersionColor;
            }
        }
        private void ChangeType()
        {
            if(type == "Tool")
            {
                panel_dockerCaption.Visible = false;
                panel_middler.Top = 3;
                this.Text = "Configure Custom Tool Project";
            }
          
        }
        private void txt_dockerCaption_TextChanged(object sender, EventArgs e)
        {
            DockerCaption = txt_dockerCaption.Text;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

        }

        private void btn_done_Click(object sender, EventArgs e)
        {

        }
        private void AddCheckBox(string text,int id,bool enabled)
        {
            CheckBox ck = new CheckBox();
            ck.Text = text;
            ck.Tag = id;
            ck.Enabled = true;
            if (enabled)
                ck.ForeColor = CkVersionColor;
            else
                ck.ForeColor = Color.Red;
            ck.AutoSize = true;
            ck.Click += Ck_Click;
            this.flowLayoutPanel_Versions.Controls.Add(ck);
        }

        private void Ck_Click(object sender, EventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            CorelVersionInfo temp = installedVersions.Find(r => r.CorelVersion == (int)ck.Tag);
            if (ck.Checked)
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
                    if (!temp.recoverPathManually(temp.CorelVersion,virtualFolder))
                        ck.Checked = false;
                   
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
            if (!ck.Checked && this.selectedVersions.Count > 0)
                this.selectedVersions.Remove(installedVersions.Find(r => r.CorelVersion == (int)ck.Tag));
            if (this.selectedVersions.Count > 0)
                btn_done.Enabled = true;
            else
                btn_done.Enabled = false;
            
        }

        private void rb_global_Click(object sender, EventArgs e)
        {
            this.GlobalTargets = rb_global.Checked;
        }
    }
}
