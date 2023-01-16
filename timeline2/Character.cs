using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace timeline2
{
    public partial class Character : Form
    {
        public Character()
        {
            InitializeComponent();

            this.Icon = timeline2.Properties.Resources.icon;

            comboBox1.Items.Add("No (" + Global.CharacterStorylineParams[Global.CurrentStorylineID, 1] + ")");
            comboBox1.Items.Add("Yes (Prince)");
            comboBox1.Items.Add("Yes (Sand Wraith)");

            if (Global.CurrentStorylineID != 256 && Global.CurrentCharacterID != 256)
            {
                if (Global.CurrentStorylineID >= 0 && Global.CurrentStorylineID <= Global.CharacterStorylineCheckpointHexData.GetLength(0))
                {
                    if (Global.SaveData[Global.PlatformOffsetHack + Global.sd_char] == Global.SaveData[Global.PlatformOffsetHack + Global.sd_storyline] && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 1)] == Global.SaveData[Global.PlatformOffsetHack + (Global.sd_storyline + 1)] && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 2)] == Global.SaveData[Global.PlatformOffsetHack + (Global.sd_storyline + 2)] && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 3)] == Global.SaveData[Global.PlatformOffsetHack + (Global.sd_storyline + 3)])
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Global.SaveData[Global.PlatformOffsetHack + Global.sd_char] == 0x48 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 1)] == 0x28 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 2)] == 0x08 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 3)] == 0x2B)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    else if (Global.SaveData[Global.PlatformOffsetHack + Global.sd_char] == 0x64 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 1)] == 0xEF && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 2)] == 0x00 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 3)] == 0x3B)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                }
            }

            
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            //WRITE CHARACTER DATA
            if (comboBox1.SelectedIndex == 0)
            {
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_char] = Global.SaveData[Global.PlatformOffsetHack + Global.sd_storyline];
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 1)] = Global.SaveData[Global.PlatformOffsetHack + (Global.sd_storyline + 1)];
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 2)] = Global.SaveData[Global.PlatformOffsetHack + (Global.sd_storyline + 2)];
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 3)] = Global.SaveData[Global.PlatformOffsetHack + (Global.sd_storyline + 3)];
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                if (Global.SavePlatform != "PS3")
                {
                    Global.SaveData[Global.PlatformOffsetHack + Global.sd_char] = 0x48;
                    Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 1)] = 0x28;
                    Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 2)] = 0x08;
                    Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 3)] = 0x2B;
                }
                else if (Global.SavePlatform == "PS3")
                {
                    Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 3)] = 0x48;
                    Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 2)] = 0x28;
                    Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 1)] = 0x08;
                    Global.SaveData[Global.PlatformOffsetHack + Global.sd_char] = 0x2B;
                }
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                if (Global.SavePlatform != "PS3")
                {
                    Global.SaveData[Global.PlatformOffsetHack + Global.sd_char] = 0x64;
                    Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 1)] = 0xEF;
                    Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 2)] = 0x00;
                    Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 3)] = 0x3B;
                }
                else if (Global.SavePlatform == "PS3")
                {
                    Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 3)] = 0x64;
                    Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 2)] = 0xEF;
                    Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 1)] = 0x00;
                    Global.SaveData[Global.PlatformOffsetHack + Global.sd_char] = 0x3B;
                }
            }

            Global.DetectCharacterStoryline();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
