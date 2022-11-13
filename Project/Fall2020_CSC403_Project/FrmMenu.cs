using Fall2020_CSC403_Project.code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class FrmMenu : Form
    {
        public static int mode = 0;
        public static int volume_level = 0;

        public FrmMenu()
        {
            InitializeComponent();
            mode = FrmSettings.mode;
            volume_level = FrmSettings.volume_level;
            if (mode == 1)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
                mode = 1;
            }
            if(mode == 2)
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                mode = 2;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmCharacterSelect menu = new FrmCharacterSelect();
            this.Hide();
            menu.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmSettings settings = new FrmSettings();
            this.Hide();
            settings.ShowDialog();
            this.Close();
        }
    }
}
