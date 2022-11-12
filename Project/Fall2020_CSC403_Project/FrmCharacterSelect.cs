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
            FrmLevel level = new FrmLevel();
            this.Hide();
            level.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            charClass = 2;
            FrmLevel level = new FrmLevel();
            this.Hide();
            level.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            charClass = 3;
            FrmLevel level = new FrmLevel();
            this.Hide();
            level.ShowDialog();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            username = textBox1.Text.ToString();
        }
    }
}
