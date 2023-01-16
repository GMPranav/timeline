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
    public partial class Difficulty : Form
    {
        public Difficulty()
        {
            InitializeComponent();

            this.Icon = timeline2.Properties.Resources.icon;

            for (int i = 0; i < 3; i++)
            {
                comboBox1.Items.Add(Global.DifficultyParams[i]);
            }

            if (Global.CurrentDifficulty != 256)
            {
                comboBox1.SelectedIndex = Global.CurrentDifficulty;
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Global.SaveData[Global.PlatformOffsetHack + Global.sd_difficulty] = Convert.ToByte(comboBox1.SelectedIndex);

            Global.DetectDifficulty();

            if (Global.SavePlatform != "PS3")
            {
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_currentlife] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, Global.CurrentLifeID, 0]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_currentlife + 1)] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, Global.CurrentLifeID, 1]);

                Global.SaveData[Global.PlatformOffsetHack + Global.sd_maxlife] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, Global.MaxLifeID, 0]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_maxlife + 1)] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, Global.MaxLifeID, 1]);
            }
            else if (Global.SavePlatform == "PS3")
            {
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_currentlife + 1)] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, Global.CurrentLifeID, 0]);
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_currentlife] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, Global.CurrentLifeID, 1]);

                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_maxlife + 1)] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, Global.MaxLifeID, 0]);
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_maxlife] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, Global.MaxLifeID, 1]);
            }

            Global.DetectLife();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
