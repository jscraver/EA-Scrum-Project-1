﻿namespace Fall2020_CSC403_Project
{
    partial class FrmMenu
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.picPlayer = new System.Windows.Forms.PictureBox();
<<<<<<< HEAD
            this.comboBox1 = new System.Windows.Forms.ComboBox();
=======
>>>>>>> 35db04980f4796e146402d742408ec568ab46140
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Playbill", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Location = new System.Drawing.Point(282, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(603, 145);
            this.label1.TabIndex = 0;
            this.label1.Text = "PEANUT GAME";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Font = new System.Drawing.Font("Playbill", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(438, 366);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(302, 122);
            this.button1.TabIndex = 1;
            this.button1.Text = "PLAY";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // picPlayer
            // 
            this.picPlayer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picPlayer.BackColor = System.Drawing.Color.Transparent;
            this.picPlayer.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.player;
            this.picPlayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picPlayer.Location = new System.Drawing.Point(102, 318);
            this.picPlayer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picPlayer.Name = "picPlayer";
            this.picPlayer.Size = new System.Drawing.Size(198, 332);
            this.picPlayer.TabIndex = 3;
            this.picPlayer.TabStop = false;
            // 
<<<<<<< HEAD
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Playbill", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ItemHeight = 98;
            this.comboBox1.Items.AddRange(new object[] {
            "Grassy Field",
            "Deep Mine",
            "Tundra",
            "Custom Level"});
            this.comboBox1.Location = new System.Drawing.Point(397, 604);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(380, 106);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.Level_Updated);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Font = new System.Drawing.Font("Playbill", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(438, 494);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(302, 104);
            this.button2.TabIndex = 5;
            this.button2.Text = "EDIT LEVEL";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
=======
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.AutoSize = true;
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Font = new System.Drawing.Font("Playbill", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(438, 528);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(302, 122);
            this.button2.TabIndex = 4;
            this.button2.Text = "SETTINGS";
            this.button2.UseVisualStyleBackColor = false;
>>>>>>> 35db04980f4796e146402d742408ec568ab46140
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Goldenrod;
            this.ClientSize = new System.Drawing.Size(1204, 849);
            this.Controls.Add(this.button2);
<<<<<<< HEAD
            this.Controls.Add(this.comboBox1);
=======
>>>>>>> 35db04980f4796e146402d742408ec568ab46140
            this.Controls.Add(this.picPlayer);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "FrmMenu";
            this.Text = "Main Menu";
            ((System.ComponentModel.ISupportInitialize)(this.picPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox picPlayer;
<<<<<<< HEAD
        private System.Windows.Forms.ComboBox comboBox1;
=======
>>>>>>> 35db04980f4796e146402d742408ec568ab46140
        private System.Windows.Forms.Button button2;
    }
}