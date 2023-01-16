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
    public partial class Life : Form
    {
        public Life()
        {
            InitializeComponent();

            this.Icon = timeline2.Properties.Resources.icon;

            textBox1.Text = (Global.CurrentLife[0] + Global.CurrentLife[1] * 256).ToString();
            textBox2.Text = (Global.MaxLife[0] + Global.MaxLife[1] * 256).ToString();

            for (int i = 0; i < 10; i++)
            {
                comboBox1.Items.Add((Global.LifeHexData[Global.CurrentDifficulty, i, 0] + Global.LifeHexData[Global.CurrentDifficulty, i, 1] * 256).ToString());
                comboBox2.Items.Add((Global.LifeHexData[Global.CurrentDifficulty, i, 0] + Global.LifeHexData[Global.CurrentDifficulty, i, 1] * 256).ToString());
            }

            if (Global.CurrentLifeID >=0 && Global.CurrentLifeID<=9) comboBox1.SelectedIndex = Global.CurrentLifeID;
            if (Global.MaxLifeID >= 0 && Global.MaxLifeID <= 9) comboBox2.SelectedIndex = Global.MaxLifeID;
           
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (Global.SavePlatform != "PS3")
            {
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_currentlife] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, comboBox1.SelectedIndex, 0]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_currentlife + 1)] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, comboBox1.SelectedIndex, 1]);

                Global.SaveData[Global.PlatformOffsetHack + Global.sd_maxlife] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, comboBox2.SelectedIndex, 0]);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_maxlife + 1)] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, comboBox2.SelectedIndex, 1]);
            }
            else if (Global.SavePlatform == "PS3")
            {
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_currentlife + 1)] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, comboBox1.SelectedIndex, 0]);
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_currentlife] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, comboBox1.SelectedIndex, 1]);

                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_maxlife + 1)] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, comboBox2.SelectedIndex, 0]);
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_maxlife] = Convert.ToByte(Global.LifeHexData[Global.CurrentDifficulty, comboBox2.SelectedIndex, 1]);
            }

            Global.DetectLife();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_edit_custom_Click(object sender, EventArgs e)
        {
            if (Global.SavePlatform != "PS3")
            {
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_currentlife] = Convert.ToByte(Convert.ToInt32(textBox1.Text) % 256);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_currentlife + 1)] = Convert.ToByte(Convert.ToInt32(textBox1.Text) / 256);

                Global.SaveData[Global.PlatformOffsetHack + Global.sd_maxlife] = Convert.ToByte(Convert.ToInt32(textBox2.Text) % 256);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_maxlife + 1)] = Convert.ToByte(Convert.ToInt32(textBox2.Text) / 256);
            }
            else if (Global.SavePlatform == "PS3")
            {
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_currentlife + 1)] = Convert.ToByte(Convert.ToInt32(textBox1.Text) % 256);
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_currentlife] = Convert.ToByte(Convert.ToInt32(textBox1.Text) / 256);

                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_maxlife + 1)] = Convert.ToByte(Convert.ToInt32(textBox2.Text) % 256);
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_maxlife] = Convert.ToByte(Convert.ToInt32(textBox2.Text) / 256);
            }

            Global.DetectLife();
        }
    }
}
