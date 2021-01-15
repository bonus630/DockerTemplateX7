namespace Wizard001
{
    partial class ConfigureForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel_dockerCaption = new System.Windows.Forms.Panel();
            this.txt_dockerCaption = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_middler = new System.Windows.Forms.Panel();
            this.flowLayoutPanel_Versions = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_done = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rb_global = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel_dockerCaption.SuspendLayout();
            this.panel_middler.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_dockerCaption
            // 
            this.panel_dockerCaption.Controls.Add(this.txt_dockerCaption);
            this.panel_dockerCaption.Controls.Add(this.label1);
            this.panel_dockerCaption.Location = new System.Drawing.Point(12, 3);
            this.panel_dockerCaption.Name = "panel_dockerCaption";
            this.panel_dockerCaption.Size = new System.Drawing.Size(402, 59);
            this.panel_dockerCaption.TabIndex = 5;
            // 
            // txt_dockerCaption
            // 
            this.txt_dockerCaption.Location = new System.Drawing.Point(2, 24);
            this.txt_dockerCaption.Name = "txt_dockerCaption";
            this.txt_dockerCaption.Size = new System.Drawing.Size(384, 20);
            this.txt_dockerCaption.TabIndex = 4;
            this.txt_dockerCaption.Text = "Enter Caption";
            this.txt_dockerCaption.TextChanged += new System.EventHandler(this.txt_dockerCaption_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(-1, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Docker Caption:";
            // 
            // panel_middler
            // 
            this.panel_middler.Controls.Add(this.flowLayoutPanel_Versions);
            this.panel_middler.Controls.Add(this.label2);
            this.panel_middler.Location = new System.Drawing.Point(12, 68);
            this.panel_middler.Name = "panel_middler";
            this.panel_middler.Size = new System.Drawing.Size(392, 338);
            this.panel_middler.TabIndex = 6;
            // 
            // flowLayoutPanel_Versions
            // 
            this.flowLayoutPanel_Versions.Location = new System.Drawing.Point(5, 31);
            this.flowLayoutPanel_Versions.Name = "flowLayoutPanel_Versions";
            this.flowLayoutPanel_Versions.Size = new System.Drawing.Size(231, 304);
            this.flowLayoutPanel_Versions.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(2, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Add one or more CorelDraw Version(s) to your Project:";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(347, 412);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(52, 23);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // btn_done
            // 
            this.btn_done.Enabled = false;
            this.btn_done.Location = new System.Drawing.Point(241, 412);
            this.btn_done.Name = "btn_done";
            this.btn_done.Size = new System.Drawing.Size(91, 23);
            this.btn_done.TabIndex = 7;
            this.btn_done.Text = "Done";
            this.btn_done.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rb_global);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Location = new System.Drawing.Point(12, 412);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(223, 23);
            this.panel1.TabIndex = 8;
            // 
            // rb_global
            // 
            this.rb_global.AutoSize = true;
            this.rb_global.Checked = true;
            this.rb_global.Location = new System.Drawing.Point(3, 3);
            this.rb_global.Name = "rb_global";
            this.rb_global.Size = new System.Drawing.Size(55, 17);
            this.rb_global.TabIndex = 0;
            this.rb_global.TabStop = true;
            this.rb_global.Text = "Global";
            this.toolTip1.SetToolTip(this.rb_global, "Store CorelDraw Paths in a Targets File in %localappdata%\\bonus630, common for al" +
        "l projects");
            this.rb_global.UseVisualStyleBackColor = true;
            this.rb_global.Click += new System.EventHandler(this.rb_global_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(96, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(51, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "Local";
            this.toolTip1.SetToolTip(this.radioButton1, "Store CorelDraw Paths in a Targets File in $(SolutionDir) unique for each project" +
        "");
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Click += new System.EventHandler(this.rb_global_Click);
            // 
            // ConfigureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(418, 447);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_middler);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.panel_dockerCaption);
            this.Controls.Add(this.btn_done);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigureForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure Docker Project";
            this.panel_dockerCaption.ResumeLayout(false);
            this.panel_dockerCaption.PerformLayout();
            this.panel_middler.ResumeLayout(false);
            this.panel_middler.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_dockerCaption;
        private System.Windows.Forms.TextBox txt_dockerCaption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_middler;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Versions;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_done;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rb_global;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

