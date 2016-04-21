namespace Wizard001
{
    partial class Form1
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
            this.rd_corelX7 = new System.Windows.Forms.RadioButton();
            this.rd_corelX8 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_dockerCaption = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rd_corelX7
            // 
            this.rd_corelX7.AutoSize = true;
            this.rd_corelX7.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.rd_corelX7.Location = new System.Drawing.Point(16, 81);
            this.rd_corelX7.Name = "rd_corelX7";
            this.rd_corelX7.Size = new System.Drawing.Size(90, 17);
            this.rd_corelX7.TabIndex = 0;
            this.rd_corelX7.TabStop = true;
            this.rd_corelX7.Text = "CorelDraw X7";
            this.rd_corelX7.UseVisualStyleBackColor = true;
            // 
            // rd_corelX8
            // 
            this.rd_corelX8.AutoSize = true;
            this.rd_corelX8.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.rd_corelX8.Location = new System.Drawing.Point(164, 81);
            this.rd_corelX8.Name = "rd_corelX8";
            this.rd_corelX8.Size = new System.Drawing.Size(90, 17);
            this.rd_corelX8.TabIndex = 0;
            this.rd_corelX8.TabStop = true;
            this.rd_corelX8.Text = "CorelDraw X8";
            this.rd_corelX8.UseVisualStyleBackColor = true;
            this.rd_corelX8.CheckedChanged += new System.EventHandler(this.rd_corelX8_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Docker Caption:";
            // 
            // txt_dockerCaption
            // 
            this.txt_dockerCaption.Location = new System.Drawing.Point(16, 30);
            this.txt_dockerCaption.Name = "txt_dockerCaption";
            this.txt_dockerCaption.Size = new System.Drawing.Size(258, 20);
            this.txt_dockerCaption.TabIndex = 2;
            this.txt_dockerCaption.Text = "Enter Caption";
            this.txt_dockerCaption.TextChanged += new System.EventHandler(this.txt_dockerCaption_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(13, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Version:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(286, 111);
            this.Controls.Add(this.txt_dockerCaption);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rd_corelX8);
            this.Controls.Add(this.rd_corelX7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure Docker Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rd_corelX7;
        private System.Windows.Forms.RadioButton rd_corelX8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_dockerCaption;
        private System.Windows.Forms.Label label1;
    }
}

