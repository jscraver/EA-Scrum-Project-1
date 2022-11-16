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
        public FrmMenu()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string levelstring = GetLevelString(comboBox1.SelectedIndex);

            FrmLevel level = new FrmLevel(levelstring);
            this.Hide();
            level.ShowDialog();
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
                button2.Visible = true;
            else
                button2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Visible == true)
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
