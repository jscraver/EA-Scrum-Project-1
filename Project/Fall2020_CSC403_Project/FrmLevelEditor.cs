using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;

namespace Fall2020_CSC403_Project
{
    public partial class FrmLevelEditor : Form
    {
        private string levelstring;
        private char[,][] level;
        const int LEVEL_ROW_SIZE = 10;
        const int LEVEL_COLUMN_SIZE = 17;

        PictureBox currentlyselected;

        public FrmLevelEditor(string levelstring)
        {
            InitializeComponent();
            this.levelstring = levelstring;
        }

        private void FrmLevelEditor_Load(object sender, EventArgs e)
        {
            int rownumber = 0;
            int columnnumber = 0;

            level = new char[LEVEL_ROW_SIZE, LEVEL_COLUMN_SIZE][];
            foreach (string row in levelstring.Split('\n'))
            {
                for (int hexindex = 0; hexindex < row.Length - 1; hexindex += 2)
                {
                    string mapdatabinary = ConvertCharToBinary(row[hexindex]) + ConvertCharToBinary(row[hexindex + 1]);
                    char[] tilecode = mapdatabinary.ToCharArray();
                    level[rownumber, hexindex / 2] = tilecode;
                    columnnumber++;
                }
                rownumber++;
            }

            for (int index = 0; index < LEVEL_ROW_SIZE * LEVEL_COLUMN_SIZE; index++)
            {
                PictureBox tilepicture = Controls.Find("tile" + (index + 1).ToString(), true)[0] as PictureBox;
                char[] tilecode = level[index / LEVEL_COLUMN_SIZE, index % LEVEL_COLUMN_SIZE];
                string tiletexturecode = String.Join("", tilecode.Take(3).ToArray());
                switch (tiletexturecode)
                {
                    case "000": tilepicture.BackgroundImage = Properties.Resources.Brick_Texture; break;
                    case "001": tilepicture.BackgroundImage = Properties.Resources.Dirt_Texture; break;
                    case "010": tilepicture.BackgroundImage = Properties.Resources.Grass_Texture; break;
                    case "011": tilepicture.BackgroundImage = Properties.Resources.Ice_Texture; break;
                    case "100": tilepicture.BackgroundImage = Properties.Resources.Lava_Texture; break;
                    case "101": tilepicture.BackgroundImage = Properties.Resources.Portal_Texture; break;
                    case "110": tilepicture.BackgroundImage = Properties.Resources.Stone_Texture; break;
                    case "111": tilepicture.BackgroundImage = Properties.Resources.Water_Texture; break;
                }
            }
        }
        private void Mouse_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                PictureBox pictureBox = sender as PictureBox;
                currentlyselected = pictureBox;
                int tilenumber = Int32.Parse(pictureBox.Name.Substring(4))-1;
                char[] tilecode = level[tilenumber / LEVEL_COLUMN_SIZE, tilenumber % LEVEL_COLUMN_SIZE];
                checkedListBox1.ItemCheck -= CheckBox_Ticked;

                checkedListBox1.SetItemChecked(0, tilecode[3] == '1' ? true : false);
                checkedListBox1.SetItemChecked(1, tilecode[4] == '1' ? true : false);
                checkedListBox1.SetItemChecked(2, tilecode[5] == '1' ? true : false);
                checkedListBox1.SetItemChecked(3, tilecode[6] == '1' ? true : false);
                checkedListBox1.SetItemChecked(4, tilecode[7] == '1' ? true : false);
                //checkedListBox1.SetItemChecked(5, tilecode[8] == 1 ? true : false);
                //checkedListBox1.SetItemChecked(6, tilecode[9] == 1 ? true : false);

                checkedListBox1.ItemCheck += CheckBox_Ticked;
                string tiletexturecode = String.Join("", tilecode.Take(3).ToArray());
                comboBox1.SelectedIndex = Convert.ToInt32(tiletexturecode, 2);
            }
        }

        private void SelectedIndex_Changed(object sender, EventArgs e)
        {

            switch (comboBox1.SelectedIndex)
            {
                case 0: currentlyselected.BackgroundImage = Properties.Resources.Brick_Texture; break;
                case 1: currentlyselected.BackgroundImage = Properties.Resources.Dirt_Texture; break;
                case 2: currentlyselected.BackgroundImage = Properties.Resources.Grass_Texture; break;
                case 3: currentlyselected.BackgroundImage = Properties.Resources.Ice_Texture; break;
                case 4: currentlyselected.BackgroundImage = Properties.Resources.Lava_Texture; break;
                case 5: currentlyselected.BackgroundImage = Properties.Resources.Portal_Texture; break;
                case 6: currentlyselected.BackgroundImage = Properties.Resources.Stone_Texture; break;
                case 7: currentlyselected.BackgroundImage = Properties.Resources.Water_Texture; break;
            }
            int tilenumber = Int32.Parse(currentlyselected.Name.Substring(4)) - 1;
            int rownumber = tilenumber / LEVEL_COLUMN_SIZE;
            int columnnumber = tilenumber % LEVEL_COLUMN_SIZE;
            char[] texturecode = Convert.ToString(comboBox1.SelectedIndex, 2).PadLeft(3, '0').ToCharArray();
            for (int i = 0; i < texturecode.Length; i++)
                level[rownumber, columnnumber][i] = texturecode[i];
        }

        private void CheckBox_Ticked(object sender, EventArgs e)
        {
            checkedListBox1.ItemCheck -= CheckBox_Ticked;
            checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, !checkedListBox1.GetItemChecked(checkedListBox1.SelectedIndex));
            checkedListBox1.ItemCheck += CheckBox_Ticked;

            int tilenumber = Int32.Parse(currentlyselected.Name.Substring(4)) - 1;
            int rownumber = tilenumber / LEVEL_COLUMN_SIZE;
            int columnnumber = tilenumber % LEVEL_COLUMN_SIZE;
            for(int i = 3; i < 8; i++)
                level[rownumber, columnnumber][i] = checkedListBox1.GetItemChecked(i-3) ? '1': '0';
        }

        private void SaveIcon_Clicked(object sender, EventArgs e)
        {
            string map = "";
            for (int i = 0; i < LEVEL_ROW_SIZE; i++)
            {
                for (int j = 0; j < LEVEL_COLUMN_SIZE; j++)
                {
                    char[] charcodes = new char[2];
                    charcodes[0] = ConvertBinaryToChar(String.Join("",level[i, j].Take(4).ToArray()));
                    charcodes[1] = ConvertBinaryToChar(String.Join("", level[i, j].Skip(4).Take(4).ToArray()));
                    map += charcodes[0] + "" + charcodes[1];
                }
                map += "\n";
            }
            File.WriteAllText(@"..\..\Properties\Maps\CustomLevel.txt", map);
            this.Close();
        }

        private String ConvertCharToBinary(char c)
        {
            switch (c)
            {
                case '0': return "0000";
                case '1': return "0001";
                case '2': return "0010";
                case '3': return "0011";
                case '4': return "0100";
                case '5': return "0101";
                case '6': return "0110";
                case '7': return "0111";
                case '8': return "1000";
                case '9': return "1001";
                case 'A': return "1010";
                case 'B': return "1011";
                case 'C': return "1100";
                case 'D': return "1101";
                case 'E': return "1110";
                case 'F': return "1111";
                default: return "0000";
            }
        }

        private char ConvertBinaryToChar(string b)
        {
            switch (b)
            {
                case "0000": return '0';
                case "0001": return '1';
                case "0010": return '2';
                case "0011": return '3';
                case "0100": return '4';
                case "0101": return '5';
                case "0110": return '6';
                case "0111": return '7';
                case "1000": return '8';
                case "1001": return '9';
                case "1010": return 'A';
                case "1011": return 'B';
                case "1100": return 'C';
                case "1101": return 'D';
                case "1110": return 'E';
                case "1111": return 'F';
                default: return '0';
            }
        }
    }
}
