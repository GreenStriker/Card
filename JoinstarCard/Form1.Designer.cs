﻿namespace JoinstarCard
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.croppablePictureBox1 = new JoinstarCard.CroppablePictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.croppablePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // croppablePictureBox1
            // 
            this.croppablePictureBox1.BackColor = System.Drawing.SystemColors.Desktop;
            this.croppablePictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("croppablePictureBox1.Image")));
            this.croppablePictureBox1.Location = new System.Drawing.Point(13, 13);
            this.croppablePictureBox1.Name = "croppablePictureBox1";
            this.croppablePictureBox1.Size = new System.Drawing.Size(640, 480);
            this.croppablePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.croppablePictureBox1.TabIndex = 0;
            this.croppablePictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(300, 512);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Save cropped part";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(433, 509);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select face by left click + mouse dragging.\r\nEnlarge selection area by dragging a" +
    " corner.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 550);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.croppablePictureBox1);
            this.Name = "Form1";
            this.Text = "CroppablePictureBox sample by Petusa";
            ((System.ComponentModel.ISupportInitialize)(this.croppablePictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        public CroppablePictureBox croppablePictureBox1;
    }
}

