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
    public partial class Storyline : Form
    {
        public Storyline()
        {
            InitializeComponent();

            this.Icon = timeline2.Properties.Resources.icon;

            for (int i = 0; i < Global.CharacterStorylineCheckpointHexData.GetLength(0); i++)
            {
                if (Global.CharacterStorylineParams[i, 0] != null && Global.CharacterStorylineParams[i, 1] != null)
                {
                    if (i <= 8)
                    {
                        comboBox1.Items.Add("0" + (i + 1) + " - " + Global.CharacterStorylineParams[i, 0]);
                        //comboBox2.Items.Add("0" + (i + 1) + " - " + Global.CharacterStorylineParams[i, 1]);
                    }
                    else if (i >= 9 && i <= 98)
                    {
                        comboBox1.Items.Add((i + 1) + " - " + Global.CharacterStorylineParams[i, 0]);
                        //comboBox2.Items.Add((i + 1) + " - " + Global.CharacterStorylineParams[i, 1]);
                    }
                }
            }

            if (Global.CurrentStorylineID != 256 && Global.CurrentCharacterID != 256)
            {
                if (Global.CurrentStorylineID >= 0 && Global.CurrentStorylineID <= Global.CharacterStorylineCheckpointHexData.GetLength(0))
                {
                    comboBox1.SelectedIndex = Global.CurrentStorylineID;
                    //comboBox2.SelectedIndex = Global.CurrentCharacterID;
                }
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            //WRITE STORYLINE DATA
            Global.SaveData[Global.PlatformOffsetHack + Global.sd_storyline] = Convert.ToByte(Global.CharacterStorylineCheckpointHexData[comboBox1.SelectedIndex, 0]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_storyline + 1)] = Convert.ToByte(Global.CharacterStorylineCheckpointHexData[comboBox1.SelectedIndex, 1]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_storyline + 2)] = Convert.ToByte(Global.CharacterStorylineCheckpointHexData[comboBox1.SelectedIndex, 2]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_storyline + 3)] = Convert.ToByte(Global.CharacterStorylineCheckpointHexData[comboBox1.SelectedIndex, 3]);

            //WRITE CHARACTER DATA
            Global.SaveData[Global.PlatformOffsetHack + Global.sd_char] = Convert.ToByte(Global.CharacterStorylineCheckpointHexData[comboBox1.SelectedIndex, 0]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 1)] = Convert.ToByte(Global.CharacterStorylineCheckpointHexData[comboBox1.SelectedIndex, 1]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 2)] = Convert.ToByte(Global.CharacterStorylineCheckpointHexData[comboBox1.SelectedIndex, 2]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 3)] = Convert.ToByte(Global.CharacterStorylineCheckpointHexData[comboBox1.SelectedIndex, 3]);

            Global.DetectCharacterStoryline();

            if (checkBox1.Checked == true)
            {
                //WRITE LOCATION DATA
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_checkpoint] = Convert.ToByte(Global.CharacterStorylineCheckpointHexData[comboBox1.SelectedIndex, 0]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_checkpoint + 1)] = Convert.ToByte(Global.CharacterStorylineCheckpointHexData[comboBox1.SelectedIndex, 1]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_checkpoint + 2)] = Convert.ToByte(Global.CharacterStorylineCheckpointHexData[comboBox1.SelectedIndex, 2]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_checkpoint + 3)] = Convert.ToByte(Global.CharacterStorylineCheckpointHexData[comboBox1.SelectedIndex, 3]);

                Global.SaveData[Global.PlatformOffsetHack + Global.sd_location] = Convert.ToByte(Global.CharacterStorylineLocationHexData[comboBox1.SelectedIndex, 0]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_location + 1)] = Convert.ToByte(Global.CharacterStorylineLocationHexData[comboBox1.SelectedIndex, 1]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_location + 2)] = Convert.ToByte(Global.CharacterStorylineLocationHexData[comboBox1.SelectedIndex, 2]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_location + 3)] = Convert.ToByte(Global.CharacterStorylineLocationHexData[comboBox1.SelectedIndex, 3]);

                Global.DetectLocation();
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
