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
                string tiletexturecode2 = String.Join("", tilecode.Skip(9).Take(1).ToArray());

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
                if (tiletexturecode2 == "1" && tilepicture.BackgroundImage == Properties.Resources.Grass_Texture) { 
                    tilepicture.BackgroundImage = Properties.Resources.GrassSwordTexture;
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
                checkedListBox1.SetItemChecked(1, tilecode[5] == '1' ? true : false);
                checkedListBox1.SetItemChecked(2, tilecode[6] == '1' ? true : false);
                checkedListBox1.SetItemChecked(3, tilecode[7] == '1' ? true : false);
                checkedListBox1.SetItemChecked(4, tilecode[8] == '1' ? true : false);

                checkedListBox1.ItemCheck += CheckBox_Ticked;
                string tiletexturecode = String.Join("", tilecode.Take(3).ToArray());
                if (tilecode[9] == '1') 
                    comboBox1.SelectedIndex = 8;
                else
                    comboBox1.SelectedIndex = Convert.ToInt32(tiletexturecode, 2);
            }
        }

        private void SelectedIndex_Changed(object sender, EventArgs e)
        {
            char[] texturecode;

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
                case 8: currentlyselected.BackgroundImage = Properties.Resources.GrassSwordTexture; break;
            }
            int tilenumber = Int32.Parse(currentlyselected.Name.Substring(4)) - 1;
            int rownumber = tilenumber / LEVEL_COLUMN_SIZE;
            int columnnumber = tilenumber % LEVEL_COLUMN_SIZE;

            if (comboBox1.SelectedIndex == 8)
            {
                texturecode = new char[] { '0', '1', '0' };
                level[rownumber, columnnumber][9] = '1';
            }
            else
            {
                texturecode = Convert.ToString(comboBox1.SelectedIndex, 2).PadLeft(3, '0').ToCharArray();
                level[rownumber, columnnumber][9] = '0';
            }

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

            level[rownumber, columnnumber][3] = checkedListBox1.GetItemChecked(0) ? '1' : '0';
            for (int i = 5; i < 9; i++)
                level[rownumber, columnnumber][i] = checkedListBox1.GetItemChecked(i-4) ? '1': '0';
        }

        private void SaveIcon_Clicked(object sender, EventArgs e)
        {
            string map = "";
            for (int i = 0; i < LEVEL_ROW_SIZE; i++)
            {
                for (int j = 0; j < LEVEL_COLUMN_SIZE; j++)
                {
                    char[] charcodes = new char[2];
                    level[i, j][4] = level[i, j][9];
                    charcodes[0] = ConvertBinaryToChar(String.Join("",level[i, j].Take(5).ToArray()));
                    charcodes[1] = ConvertBinaryToChar(String.Join("", level[i, j].Skip(5).Take(5).ToArray()));
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
                case '0': return "00000";
                case '1': return "00001";
                case '2': return "00010";
                case '3': return "00011";
                case '4': return "00100";
                case '5': return "00101";
                case '6': return "00110";
                case '7': return "00111";
                case '8': return "01000";
                case '9': return "01001";
                case 'A': return "01010";
                case 'B': return "01011";
                case 'C': return "01100";
                case 'D': return "01101";
                case 'E': return "01110";
                case 'F': return "01111";
                case 'G': return "10000";
                case 'H': return "10001";
                case 'I': return "10010";
                case 'J': return "10011";
                case 'K': return "10100";
                case 'L': return "10101";
                case 'M': return "10110";
                case 'N': return "10111";
                case 'O': return "11000";
                case 'P': return "11001";
                case 'Q': return "11010";
                case 'R': return "11011";
                case 'S': return "11100";
                case 'T': return "11101";
                case 'U': return "11110";
                case 'V': return "11111";
                default: return "00000";
            }
        }

        private char ConvertBinaryToChar(string b)
        {
            switch (b)
            {
                case "00000": return '0';
                case "00001": return '1';
                case "00010": return '2';
                case "00011": return '3';
                case "00100": return '4';
                case "00101": return '5';
                case "00110": return '6';
                case "00111": return '7';
                case "01000": return '8';
                case "01001": return '9';
                case "01010": return 'A';
                case "01011": return 'B';
                case "01100": return 'C';
                case "01101": return 'D';
                case "01110": return 'E';
                case "01111": return 'F';
                case "10000": return 'G';
                case "10001": return 'H';
                case "10010": return 'I';
                case "10011": return 'J';
                case "10100": return 'K';
                case "10101": return 'L';
                case "10110": return 'M';
                case "10111": return 'N';
                case "11000": return 'O';
                case "11001": return 'P';
                case "11010": return 'Q';
                case "11011": return 'R';
                case "11100": return 'S';
                case "11101": return 'T';
                case "11110": return 'U';
                case "11111": return 'V';
                default: return '0';
            }
        }
    }
}
