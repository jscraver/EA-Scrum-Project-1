using Fall2020_CSC403_Project.code;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
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
            comboBox1.SelectedIndex = 0;
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
            string levelstring = GetLevelString(comboBox1.SelectedIndex);
            FrmCharacterSelect menu = new FrmCharacterSelect(levelstring);
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
        private string GetLevelString(int selected)
        {
            string levelstring = "";
            string path = "";
            switch (selected)
            {
                case 0: path = @"..\..\Properties\Maps\GrassyField.txt"; break;
                case 1: path = @"..\..\Properties\Maps\DeepMine.txt"; break;
                case 2: path = @"..\..\Properties\Maps\Tundra.txt"; break;
                case 3: path = @"..\..\Properties\Maps\CustomLevel.txt"; break;
                default: path = @"..\..\Properties\Maps\GrassyField.txt"; break;
            }
            levelstring = File.ReadAllText(path);
            return levelstring;
        }
        private void Level_Updated(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 3)
                button4.Visible = true;
            else
                button4.Visible = false;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (button4.Visible == true)
            {
                string levelstring = GetLevelString(comboBox1.SelectedIndex);

                FrmLevelEditor level = new FrmLevelEditor(levelstring);
                this.Hide();
                level.ShowDialog();
                this.Show();
            }
        }
    }
}
