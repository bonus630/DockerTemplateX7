using System;
using System.Drawing;
using System.Windows.Forms;


namespace Wizard001
{
    public partial class Form1 : Form
    {
        public int CorelVersion { get; private set; }
        public string DockerCaption { get; private set; }

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(45, 45, 48);
            txt_dockerCaption.Focus();
            DockerCaption = txt_dockerCaption.Text;
        }
              
        private void rd_corelX8_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_corelX7.Checked)
                CorelVersion = 17;
            if (rd_corelX8.Checked)
                CorelVersion = 18;
        }

        private void txt_dockerCaption_TextChanged(object sender, EventArgs e)
        {
            DockerCaption = txt_dockerCaption.Text;
        }
    }
}
