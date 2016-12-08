﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace Wizard001
{
    public partial class Form1 : Form
    {
        public int CorelVersion { get; private set; }
        public string DockerCaption { get; private set; }
        private List<CorelVersionInfo> installedVersions = new List<CorelVersionInfo>();
        private CorelVersionInfo corelVersionSelected;
        public CorelVersionInfo CorelVersionSelected{ get { return this.corelVersionSelected; }
            private set {
                this.corelVersionSelected = value;
                this.btn_done.Enabled = !value.CorelInstallationNotFound;
                        
                        } }
        public Form1()
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
                AddCheckBox(temp.Corel64FullName,i,!temp.CorelInstallationNotFound);
               
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
            RadioButton ck = new RadioButton();
            ck.Text = text;
            ck.Tag = id;
            ck.Enabled = true;
            if (enabled)
                ck.ForeColor = Color.Black;
            else
                ck.ForeColor = Color.Red;
            ck.AutoSize = true;
            ck.Click += Ck_Click;
            this.flowLayoutPanel_Versions.Controls.Add(ck);
        }

        private void Ck_Click(object sender, EventArgs e)
        {
            RadioButton ck = sender as RadioButton;
            this.CorelVersionSelected = installedVersions.Find(r => r.CorelVersion == (int)ck.Tag);
            if (this.CorelVersionSelected.CorelInstallationNotFound)
                this.CorelVersionSelected.recoverPathManually(this.CorelVersionSelected.CorelVersion);
        }
    }
}
