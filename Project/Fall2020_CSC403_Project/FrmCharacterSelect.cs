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
    public partial class FrmCharacterSelect : Form
    {
        public static int mode = 0;
        public static int charClass = 1;
        public static string username = "Player1";
        public static int skin = 0;

        public FrmCharacterSelect()
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
            if (trackBar1.Value == 1) skin = 1;
            FrmLevel level = new FrmLevel();
            this.Hide();
            level.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            charClass = 2;
            if (trackBar2.Value == 1) skin = 1;
            FrmLevel level = new FrmLevel();
            this.Hide();
            level.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            charClass = 3;
            if (trackBar2.Value == 1) skin = 1;
            FrmLevel level = new FrmLevel();
            this.Hide();
            level.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            username = textBox1.Text.ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value == 0)
            {
                picPlayer.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.player;
            }
            if (trackBar1.Value == 1)
            {
                picPlayer.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.player_skin;
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (trackBar2.Value == 0)
            {
                picPlayer_2.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.player_2;
            }
            if (trackBar2.Value == 1)
            {
                picPlayer_2.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.player_2_skin;
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (trackBar3.Value == 0)
            {
                picPlayer_3.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.player_3;
            }
            if (trackBar3.Value == 1)
            {
                picPlayer_3.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.player_3_skin;
            }
        }
    }
}
