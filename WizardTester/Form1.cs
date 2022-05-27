using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectHelper;

namespace WizardTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            
            //ementHost1.Child = ui;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigureUI ui = new ConfigureUI("Tool");
            ui.ShowDialog();
        }
    }
}
