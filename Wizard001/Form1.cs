using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio;

namespace Wizard001
{
    public partial class Form1 : Form
    {
        public int CorelVersion { get; private set; }
        public string DockerCaption { get; private set; }
        private List<CorelVersionInfo> installedVersions = new List<CorelVersionInfo>();
        private List<CorelVersionInfo> selectedVersions = new List<CorelVersionInfo>();
        public List<CorelVersionInfo> SelectedVersions { get { return this.selectedVersions; } }
        private VsTheme vsTheme = VsTheme.Unknown;
        private string type = "Docker";
        public Form1()
        {
            InitializeForm();
        }
      
        public Form1(VsTheme vsTheme,string type)
        {
            this.vsTheme = vsTheme;
            this.type = type;
            InitializeForm();
        }
        private void InitializeForm()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(45, 45, 48);
            txt_dockerCaption.Focus();
            DockerCaption = txt_dockerCaption.Text;
            btn_cancel.DialogResult = DialogResult.Cancel;
            btn_done.DialogResult = DialogResult.OK;

            for (int i = 17; i < 20; i++)
            {
                CorelVersionInfo temp = new CorelVersionInfo(i);
                installedVersions.Add(temp);
                AddCheckBox(temp.Corel64FullName, i, !temp.CorelInstallationNotFound);

            }
        }
        private void ChangeTheme()
        { 
            switch(vsTheme)
            {
                case VsTheme.Dark:
                    this.BackColor = Color.Black;
                break;
                case VsTheme.Blue:
                    this.BackColor = Color.Blue;
                break;
            }
        }
        private void ChangeType()
        {
            if(type == "Tool")
            {

            }
            if (type == "Docker")
            {

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
                ck.ForeColor = Color.White;
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
                    temp.recoverPathManually(temp.CorelVersion);
                this.selectedVersions.Add(temp);
            }
            if (!ck.Checked && this.selectedVersions.Count > 0)
                this.selectedVersions.Remove(installedVersions.Find(r => r.CorelVersion == (int)ck.Tag));
            if (this.selectedVersions.Count > 0)
                btn_done.Enabled = true;
            else
                btn_done.Enabled = false;
            
        }
      
    }
}
