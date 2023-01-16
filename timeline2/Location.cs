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
    public partial class Location : Form
    {
        public Location()
        {
            InitializeComponent();

            this.Icon = timeline2.Properties.Resources.icon;

            for (int i = 0; i < Global.CheckpointHexData.GetLength(0); i++)
            {
                comboBox1.Items.Add(Global.LocationCategory[i]);
            }

            if (Global.CurrentLocationCategoryID != 256 && Global.CurrentLocationID != 256)
            {
                if (Global.CurrentLocationCategoryID >= 0 && Global.CurrentLocationCategoryID <= 5)
                {
                    comboBox1.SelectedIndex = Global.CurrentLocationCategoryID;
                    comboBox2.SelectedIndex = Global.CurrentLocationID;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";


            for (int i = 0; i < Global.CheckpointHexData.GetLength(0); i++)
            {
                for (int j = 0; j < Global.CheckpointHexData.GetLength(1); j++)
                    if (comboBox1.SelectedIndex == i && Global.LocationParams[i, j, 0] != null)
                    {
                        if (j <= 8) comboBox2.Items.Add("00" + (j + 1) + " - " + Global.LocationParams[i, j, 0]);
                        else if (j >= 9 && j <= 98) comboBox2.Items.Add("0" + (j + 1) + " - " + Global.LocationParams[i, j, 0]);
                        else if (j >= 99) comboBox2.Items.Add((j + 1) + " - " + Global.LocationParams[i, j, 0]);
                    }
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Global.SaveData[Global.PlatformOffsetHack + Global.sd_checkpoint] = Convert.ToByte(Global.CheckpointHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 0]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_checkpoint + 1)] = Convert.ToByte(Global.CheckpointHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 1]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_checkpoint + 2)] = Convert.ToByte(Global.CheckpointHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 2]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_checkpoint + 3)] = Convert.ToByte(Global.CheckpointHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 3]);

            Global.SaveData[Global.PlatformOffsetHack + Global.sd_location] = Convert.ToByte(Global.LocationHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 0]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_location + 1)] = Convert.ToByte(Global.LocationHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 1]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_location + 2)] = Convert.ToByte(Global.LocationHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 2]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_location + 3)] = Convert.ToByte(Global.LocationHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 3]);

            Global.DetectLocation();

            if (checkBox1.Checked == true && comboBox1.SelectedIndex > 1)
            {
                //WRITE STORYLINE DATA
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_storyline] = Convert.ToByte(Global.CheckpointHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 0]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_storyline + 1)] = Convert.ToByte(Global.CheckpointHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 1]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_storyline + 2)] = Convert.ToByte(Global.CheckpointHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 2]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_storyline + 3)] = Convert.ToByte(Global.CheckpointHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 3]);

                //WRITE CHARACTER DATA
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_char] = Convert.ToByte(Global.CheckpointHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 0]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 1)] = Convert.ToByte(Global.CheckpointHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 1]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 2)] = Convert.ToByte(Global.CheckpointHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 2]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_char + 3)] = Convert.ToByte(Global.CheckpointHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 3]);

                Global.DetectCharacterStoryline();
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
