using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class FrmSettings : Form
    {
        public static int mode = 0;
        public static int volume_level = 50;
        public FrmSettings()
        {
            InitializeComponent();
            mode = FrmMenu.mode;
            if (mode == 1)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
                mode = 1;
            }
            if (mode == 2)
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                mode = 2;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FrmMenu menu = new FrmMenu();
            this.Hide();
            menu.ShowDialog();
            if (mode == 1) fullscreen(menu);
            if (mode == 2) windowed(menu);
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            fullscreen(this);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            windowed(this); 
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            volume_level = trackBar1.Value * 10;
        }
        private void fullscreen(Form form)
        {
            form.WindowState = FormWindowState.Normal;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.Bounds = Screen.PrimaryScreen.Bounds;
            mode = 1;
        }
        private void windowed(Form form)
        {
            form.WindowState = FormWindowState.Maximized;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            mode = 2;
        }
    }
}
